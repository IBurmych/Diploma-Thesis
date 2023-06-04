import "./App.css";
import "primereact/resources/themes/lara-light-indigo/theme.css";
import "primereact/resources/primereact.css";
import "primeicons/primeicons.css";
import "primeflex/primeflex.css";

import { Dialog } from "primereact/dialog";
import { Button } from "primereact/button";
import { useState, useCallback } from "react";
import ClientForm from "./components/clientForm";
import ClientsTable from "./components/clientsTable";

function App() {
  const [visibleNewClient, setVisibleNewClient] = useState(false);
  const [update, setUpdate] = useState(false);

  const updateClients = useCallback(() => {
    console.log("updateClients", update);
    setUpdate(true);
  }, []);
  const updatedClients = useCallback(() => {
    console.log("updatedClients");
    setUpdate(false);
  }, []);

  return (
    <div className="app">
      <header className="app-header">
        <Button
          label="Add client"
          icon="pi pi-external-link"
          onClick={() => setVisibleNewClient(true)}
        />
      </header>
      <div>
        <ClientsTable update={update} updatedClients={updatedClients} />
      </div>

      <Dialog
        header="Add new client"
        visible={visibleNewClient}
        style={{ width: "50vw" }}
        onHide={() => setVisibleNewClient(false)}
      >
        <ClientForm endpoint={"Add"} updateClients={updateClients} />
      </Dialog>
    </div>
  );
}

export default App;
