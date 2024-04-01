import React, { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import { dashboardService } from "../app-services/dashboard-service";
import Loading from "../LoadingError/Loading";
import Error from "../LoadingError/Error";

const LatestClients = () => {
    const navigate = useNavigate();
    const [error, setError] = useState(null);
    const [loading, setLoading] = useState(true);
    const [allData, setAllData] = useState({});
    const [clients, setClients] = useState([]);

    useEffect(() => {
        const fetchClients = async () => {
          setLoading(true);
          try {
            const response = await dashboardService.getDetails();
            setAllData(response);
            const casesData = response.data.clients|| [];
            setClients(casesData);
          } catch (error) {
            console.error('Error fetching clients:', error);
          } finally {
            setLoading(false); // Set loading to false after data is fetched or error occurs
          }
        };
    
        fetchClients();
      }, []); // Empty dependency array ensures the effect runs only once on mount

      const handleClientClick = (clientId) => {
        debugger;
        // Navigate to the client detail page when a client is clicked
        navigate(`/clients/${clientId}`);
      };

      return (
        <div className="card mb-4 shadow-sm">
          <div className="card-body">
            <h4 className="card-title">Latest Clients</h4>
            {loading ? (
              <Loading />
            ) : error ? (
              <Error variant="alert-danger">{error}</Error>
            ) : (
              <div className="table-responsive">
                <table className="table">
                  <tbody>
                    {clients.map((client) => (
                      <tr key={client.id} onClick={() => handleClientClick(client.id)} style={{ cursor: "pointer" }}>
                        <td>
                          <b>{client.firstName} {client.lastName}</b>
                        </td>
                        <td>{client.email}</td>
                      </tr>
                    ))}
                  </tbody>
                </table>
              </div>
            )}
          </div>
        </div>
      );
};

export default LatestClients;