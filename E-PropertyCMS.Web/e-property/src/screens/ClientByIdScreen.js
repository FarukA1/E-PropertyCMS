import React from "react"
import Header from "../components/Header";
import Main from "../components/Home/Main";
import Sidebar from "../components/Sidebar";
import ClientById from "../components/Clients/ClientById";

const ClientByIdScreen = () => {
  return (
    <>
      <Sidebar />
      <main className="main-wrap">
        <Header />
        <ClientById />
      </main>
    </>
  );
  };
  
  export default ClientByIdScreen;