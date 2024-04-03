import React, { useEffect, useState } from 'react';
import { dashboardService } from '../app-services/dashboard-service';
import TopTotal from './TopTotal';
import LatestClients from './LatestClients';
import LatestCases from './LatestCases';

const Main = () => {
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);
  const [allData, setAllData] = useState({});

  useEffect(() => {
    const fetchData = async () => {
      setLoading(true);
      try {
        const response = await dashboardService.getDetails();
        setAllData(response.data);
      } catch (error) {
        setError(error);
      } finally {
        setLoading(false);
      }
    };

    fetchData();
  }, []);

  return (
    <>
      <section className="content-main">
        <div className="content-header">
          <h2 className="content-title"> Dashboard </h2>
        </div>
        {/* Top Total */}
        <TopTotal loading={loading} error={error} data={allData || []}/>
        
          {/* STATICS */}
          <div className="row">
            <div className="col-md-6">
              {/* <LatestClients /> */}
              <LatestClients loading={loading} error={error} clients={allData.clients || []} />
            </div>
            <div className="col-md-6">
              {/* <LatestCases /> */}
              <LatestCases loading={loading} error={error} cases={allData.cases || []} />
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
