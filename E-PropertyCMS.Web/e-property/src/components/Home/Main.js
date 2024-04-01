import React, { useEffect, useState } from 'react';
import { clientService } from './../app-services/client-service';
import TopTotal from './TopTotal';
import LatestClients from './LatestClients';
import LatestCases from './LatestCases';

const Main = () => {
  // const [clients, setClients] = useState([]);

  // useEffect(() => {
  //   const fetchClients = async () => {
  //     try {
  //       const queryParams = {
  //         // Add any query parameters you need here
  //       };

  //       const response = await clientService.getClients(queryParams);
  //       setClients(response.data);
  //       clients = response.data;// Assuming the response is an array of clients
  //     } catch (error) {
  //       console.error('Error fetching clients:', error);
  //     }
  //   };

  //   fetchClients();
  // }, []); // Empty dependency array ensures the effect runs only once on mount

  return (
    <>
      <section className="content-main">
        <div className="content-header">
          <h2 className="content-title"> Dashboard </h2>
        </div>
        {/* Top Total */}
        <TopTotal />
        
          {/* STATICS */}
          <div className="row">
            <div className="col-md-6">
              <LatestClients />
            </div>
            <div className="col-md-6">
              <LatestCases />
            </div>
          </div>

        {/* LATEST ORDER */}
        {/* <div className="card mb-4 shadow-sm">
          <LatestOrder orders={orders} loading={loading} error={error} />
        </div> */}
      </section>
    </>
  );
};

export default Main;
