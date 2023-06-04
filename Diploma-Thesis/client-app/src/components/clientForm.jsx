import React, { useState, useEffect, useRef, useReducer } from "react";
import axios from "axios";
import { InputText } from "primereact/inputtext";
import { InputMask } from "primereact/inputmask";
import { Button } from "primereact/button";
import { Toast } from "primereact/toast";

export default function ClientForm({ endpoint, clientId , updateClients}) {
  const apiUploadUrl = `${process.env.REACT_APP_API_URL}Client/${endpoint}`;
  const apiGetUrl = `${process.env.REACT_APP_API_URL}Client/GetDetailed/`;
  const baseClient = {
    name: "",
    address: "",
    phone: "",
  };
  const [client, updateClient] = useReducer(
    (state, updates) => ({ ...state, ...updates }),
    baseClient
  );

  const toast = useRef(null);
  const showToast = (isSucces, detail) => {
    const severity = isSucces ? "success" : "error";
    toast.current.show({ severity, summary: "Form Submitted", detail });
  };

  useEffect(() => {
    axios
      .get(`${apiGetUrl}?id=${clientId}`)
      .then((response) => {
        updateClient(response.data);
        console.log(client)
      })
      .catch((error) => {
        showToast(false, error.message);
      });
  }, []);

  const handleSubmit = () => {
    if (clientId !== '') {
      client.Id = clientId;
    }
    axios
      .post(apiUploadUrl, client)
      .then((response) => {
        showToast(true);
        updateClients();
      })
      .catch((error) => {
        showToast(false, error.message);
      });
  };

  return (
    <>
      <Toast ref={toast} />
      <div className="card flex flex-column md:flex-row gap-3">
        <div className="p-inputgroup flex-1">
          <span className="p-inputgroup-addon">
            <i className="pi pi-user"></i>
          </span>
          <InputText
            placeholder="Name"
            value={client.name}
            onChange={(e) => updateClient({ name: e.target.value })}
          />
        </div>

        <div className="p-inputgroup flex-1">
          <span className="p-inputgroup-addon">
            <i className="pi pi-home"></i>
          </span>
          <InputText
            placeholder="Address"
            value={client.address}
            onChange={(e) => updateClient({ address: e.target.value })}
          />
        </div>

        <div className="p-inputgroup flex-1">
          <span className="p-inputgroup-addon">
            <i className="pi pi-phone"></i>
          </span>
          <InputMask
            id="phoneNumber"
            mask="+999 99 999 9999"
            placeholder="+999 99 999 9999"
            value={client.phone}
            onChange={(e) => updateClient({ phone: e.target.value })}
          />
        </div>
      </div>
      <Button
        label="Submit"
        type="submit"
        icon="pi pi-check"
        severity="success"
        onClick={handleSubmit}
      />
    </>
  );
}
