import React, { useCallback, useRef, useState, useEffect } from "react";
import axios from "axios";
import { DataTable } from "primereact/datatable";
import { Column } from "primereact/column";
import { Button } from "primereact/button";
import { LineChart } from '@mui/x-charts/LineChart';
import { InputText } from "primereact/inputtext";
import { InputNumber } from "primereact/inputnumber";

export default function InitialParameters({ toast }) {
    const getCrossingUrl = process.env.REACT_APP_API_URL + "Diapasons/GetCrossing";
    const saveCrossingUrl = process.env.REACT_APP_API_URL + "Diapasons/SaveCrosing";
    const [expandedRows, setExpandedRows] = useState(null);
    const [showGraphs, setShowGraphs] = useState(false);
    const [expName, setExpName] = useState("");
    const [diapasons, setDiapasons] = useState([
        { name: "Середнє M", normalValue: 0.32, normalDiapason: 0.022, problemValue: 0.24, problemDiapason: 0.007, isCrossing: null, difference: null },
        { name: "Дисперсія D", normalValue: 0.37, normalDiapason: 0.015, problemValue: 0.45, problemDiapason: 0.03, isCrossing: null, difference: null },
        { name: "Асиметрія A", normalValue: 13.7, normalDiapason: 1.04, problemValue: 6.9, problemDiapason: 0.45, isCrossing: null, difference: null },
        { name: "Ексцес E", normalValue: 15.4, normalDiapason: 1.17, problemValue: 44.4, problemDiapason: 4.31, isCrossing: null, difference: null },
        { name: "Перший кореляційний момент L", normalValue: 0.14, normalDiapason: 0.007, problemValue: 0.091, problemDiapason: 0.009, isCrossing: null, difference: null },
        { name: "Другий кореляційний момент S", normalValue: 0.09, normalDiapason: 0.009, problemValue: 0.13, problemDiapason: 0.008, isCrossing: null, difference: null },
        { name: "Третій кореляційний момент P", normalValue: 12.7, normalDiapason: 1.16, problemValue: 10.31, problemDiapason: 0.87, isCrossing: null, difference: null },
        { name: "Четвертий кореляційний момент Q", normalValue: 14.9, normalDiapason: 1.04, problemValue: 13.87, problemDiapason: 1.076, isCrossing: null, difference: null },
    ]);

    const changeDiapasonValue = (rowIndex, columnName, value) => {
        if(!value && value !== 0) return;

        let newDiapason = diapasons[rowIndex];
        newDiapason[columnName] = value;
        diapasons.splice(rowIndex, 1)
        diapasons.splice(rowIndex, 0, newDiapason)

        setDiapasons(diapasons);
    };
    const inputNumberTemplate = (diapason, options, columnName) => {
        return <InputNumber value={diapason[columnName]} onValueChange={(e) =>{  changeDiapasonValue(options.rowIndex, columnName, e.value)}} minFractionDigits={0} maxFractionDigits={5} ></InputNumber>;
    };
    const onClickSend = () => {
        let withoutDiff = [...diapasons];
        withoutDiff.forEach(el => el.difference = null );
        setDiapasons(withoutDiff);
        diapasons.forEach((el, index) => sendDiapason({"Element": el.normalValue, ElementRange: el.normalDiapason}, {"Element": el.problemValue, ElementRange: el.problemDiapason}, index)) 
    };
    const onClickSendExp = () => {
        const data = diapasons.map((el) => {
            return {
                Difference: el.difference,
                IsCrossing: el.isCrossing,
                Name: el.name,
                ElementRange: el.normalDiapason,
                Element: el.normalValue,
                ProblemElementRange: el.problemDiapason,
                ProblemElement: el.problemValue,
            }
        }).filter(el => !el.isCrossing);
        axios
            .post(`${saveCrossingUrl}/?name=${expName}`, data)
            .then((response) => {
                const data = response.data.map((el) =>{ return {
                    difference: el.difference,
                    isCrossing: el.isCrossing,
                    name: el.name,
                    normalValue: el.element,
                    normalDiapason: el.elementRange,
                    problemDiapason: el.problemElementRange,
                    problemValue: el.problemElement,
                    elementGradesString: el.elementGradesString.replace(", ", " - "),
                    problemElementGradesString: el.problemElementGradesString.replace(", ", " - "),
                }})
                setDiapasons(data);
            })
            .catch((error) => {
                toast.current.show({
                    severity: "error",
                    summary: "Success",
                    detail: "Notes not updated",
                });
            });
    };
    const sendDiapason = (firstDiapason, secondDiapason, index) => {
        axios
            .post(getCrossingUrl, [firstDiapason, secondDiapason])
            .then((response) => {
                changeDiapasonValue(index, "isCrossing", response.data.isCrossing);
                changeDiapasonValue(index, "difference", response.data.difference);
                
                const withFieldDifference = diapasons.filter(el => el.difference || el.difference === 0);
                
                if(withFieldDifference && withFieldDifference.length > 6){
                    setShowGraphs(true);
                }
            })
            .catch((error) => {
                toast.current.show({
                    severity: "error",
                    summary: "Success",
                    detail: "Notes not updated",
                });
            });
    }
    const rowExpansionTemplate = (data) => {
        const dataset = [
            { x: data.normalValue - data.normalDiapason, y: 0 },
            { x: data.normalValue, y: data.normalDiapason },
            { x: data.normalValue + data.normalDiapason, y: 0 },
            { x: data.problemValue - data.problemDiapason, y: 0 },
            { x: data.problemValue, y: data.problemDiapason },
            { x: data.problemValue + data.problemDiapason, y: 0 },
          ]
        return (
            <div className="card">
                <LineChart
                    dataset={dataset}
                    xAxis={[{ dataKey: 'x' }]}
                    series={[{ dataKey: 'y' }]}
                    height={300}
                    grid={{ vertical: true, horizontal: true }}
                />
            </div>
        );
    };
    

    return (
        <>
            {!showGraphs &&
                <>
                    <DataTable value={diapasons} tableStyle={{ minWidth: "50rem" }}>
                        <Column field="name" header="Характеристики" />
                        <Column field="normalValue" header="Для норми" body={(data, options) => inputNumberTemplate(data, options, "normalValue")} />
                        <Column field="normalDiapason" body={(data, options) => inputNumberTemplate(data, options, "normalDiapason")}/>
                        <Column field="problemValue" header="Для сепсису" body={(data, options) => inputNumberTemplate(data, options, "problemValue")}/>
                        <Column field="problemDiapason" body={(data, options) => inputNumberTemplate(data, options, "problemDiapason")}/>
                    </DataTable>
                    <Button label="Відправити" style={{minWidth: 150, marginTop: 10}} onClick={onClickSend}/>
                </>
            }
            {showGraphs &&
                <>
                    <DataTable 
                        value={diapasons} 
                        tableStyle={{ minWidth: "50rem" }} 
                        expandedRows={expandedRows} 
                        rowExpansionTemplate={rowExpansionTemplate}
                        onRowToggle={(e) => setExpandedRows(e.data)}
                    >
                        <Column expander={true} style={{ width: '5rem' }} />
                        <Column field="name" header="Характеристики" />
                        <Column field="difference" header="Різниця"/>
                        <Column field="isCrossing" header="Перетин" body={data => data.isCrossing?.toString() ?? "false"}/>
                        <Column field="normalValue" header="Для норми" />
                        <Column field="normalDiapason" />
                        <Column field="elementGradesString" />
                        <Column field="problemValue" header="Для сепсису"/>
                        <Column field="problemDiapason"/>
                        <Column field="problemElementGradesString"/>
                    </DataTable>
                    <InputText placeholder="Назва" style={{marginRight: 10}} onChange={e =>setExpName(e.target.value)}></InputText>
                    <Button label="Зберегти" style={{marginRight: 10, minWidth: 150, marginTop: 10}} onClick={onClickSendExp}/>
                </>
            }

        </>
    );
}