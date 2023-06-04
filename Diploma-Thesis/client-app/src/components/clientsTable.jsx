import React, { useCallback, useRef, useState, useEffect } from "react";
import axios from "axios";
import { DataTable } from "primereact/datatable";
import { Column } from "primereact/column";
import { Toast } from "primereact/toast";
import { Dialog } from "primereact/dialog";
import { Button } from "primereact/button";
import ClientForm from "./clientForm";
import Upload from "./upload";
import ExpertisesTable from "./expertisesTable";

export default function ClientsTable({ update, updatedClients }) {
  const apiUrl = `${process.env.REACT_APP_API_URL}Client/GetAll`;

  const [currentClientId, setCurrentClientId] = useState("");
  const [visibleUploadPhoto, setVisibleUploadPhoto] = useState(false);
  const [visibleUpdateClient, setVisibleUpdateClient] = useState(false);
  const [visibleExpertises, setVisibleExpertises] = useState(false);
  const [clients, setClients] = useState([]);

  const updateClients = useCallback(() => {
    getClients();
  }, []);

  const errorToast = useRef(null);
  const showErrorToast = (detail) => {
    errorToast.current.show({
      severity: "error",
      summary: "Form Submitted",
      detail,
    });
  };

  const getClients = () => {
    axios
      .get(apiUrl)
      .then((response) => {
        setClients(response.data);
      })
      .catch((error) => {
        showErrorToast(error.message);
      });
  };

  useEffect(() => {
    getClients();
  }, []);

  useEffect(() => {
    if(currentClientId === ''){
        setVisibleUploadPhoto(false);
        setVisibleUpdateClient(false);
        setVisibleExpertises(false);
    }
  }, [currentClientId]);

  useEffect(() => {
    console.log("update", update);
    if (update) {
      getClients();
      updatedClients();
    }
  }, [update]);

  const handlerUpdateClient = (id) => {
    setVisibleUpdateClient(true);
    setCurrentClientId(id);
  };
  const updateClientButton = (client) => {
    return (
      <Button
        label="Details"
        icon="pi pi-external-link"
        onClick={() => handlerUpdateClient(client.id)}
      />
    );
  };

  const handlerUploadPhoto = (id) => {
    setVisibleUploadPhoto(true);
    setCurrentClientId(id);
  };
  const uploadPhotoButton = (client) => {
    return (
    <Button
      label="Upload"
      icon="pi pi-external-link"
      onClick={() => handlerUploadPhoto(client.id)}
    />
    );
  };

  const handlerExpertises = (id) => {
    setVisibleExpertises(true);
    setCurrentClientId(id);
  };
  const showExpertises = (client) => {
    return (
    <Button
      label="Client expertises"
      icon="pi pi-external-link"
      onClick={() => handlerExpertises(client.id)}
    />
    );
  };

  return (
    <div className="table-container">
      {clients && (
        <>
          <Toast ref={errorToast} />

          <DataTable value={clients} tableStyle={{ minWidth: "50rem" }}>
            <Column field="name" header="Name" />
            <Column
              field="id"
              body={updateClientButton}
              header="Client details"
            />
            <Column
              field="id"
              body={uploadPhotoButton}
              header="Upload new expertise"
            />
            <Column
              field="id"
              body={showExpertises}
              header="Expertises"
            />
          </DataTable>
        </>
      )}
      <Dialog
        header="Details"
        visible={visibleUpdateClient}
        style={{ width: "50vw" }}
        onHide={() => setCurrentClientId("")}
      >
        <ClientForm
          endpoint={"Update"}
          clientId={currentClientId}
          updateClients={updateClients}
        />
      </Dialog>
      <Dialog
        header="Upload photo"
        visible={visibleUploadPhoto}
        style={{ width: "50vw" }}
        onHide={() => setCurrentClientId("")}
      >
        <Upload clientId={currentClientId} />
      </Dialog>
      <Dialog
        header="Expertises"
        visible={visibleExpertises}
        style={{ width: "80vw" }}
        onHide={() => setCurrentClientId("")}
      >
        <ExpertisesTable clientId={currentClientId} />
      </Dialog>
    </div>
  );
}
