import React, { useCallback, useRef, useState, useEffect } from "react";
import axios from "axios";
import { DataTable } from "primereact/datatable";
import { Column } from "primereact/column";
import { Toast } from "primereact/toast";

export default function ExpertisesTable({ clientId }) {
  const apiUrl = `${process.env.REACT_APP_API_URL}Expertises/GetByClientId/`;

  const [expertises, setExpertises] = useState([]);

  const errorToast = useRef(null);
  const showErrorToast = (detail) => {
    errorToast.current.show({
      severity: "error",
      summary: "Form Submitted",
      detail,
    });
  };

  useEffect(() => {
    if (clientId !== "") {
      getExpertises();
    }
  }, [clientId]);

  const getExpertises = () => {
    axios
      .get(`${apiUrl}?id=${clientId}`)
      .then((response) => {
        setExpertises(response.data);
      })
      .catch((error) => {
        showErrorToast(error.message);
      });
  };

  return (
    <div className="expertises-container">
      <Toast ref={errorToast} />

      <DataTable value={expertises} tableStyle={{ minWidth: "50rem" }}>
        <Column field="date" header="Date" />
        <Column field="result" header="Result" />
        <Column field="notes" header="Notes" />
      </DataTable>
    </div>
  );
}
