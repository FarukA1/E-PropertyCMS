import React from "react"
import Header from "../components/Header";
import Main from "../components/Home/Main";
import Sidebar from "../components/Sidebar";
import MainClients from "../components/Clients/MainClients";

const ClientScreen = () => {
    return (
      <>
        <Sidebar />
        <main className="main-wrap">
          <Header />
          <MainClients />
        </main>
      </>
    );
  };
  
  export default ClientScreen;