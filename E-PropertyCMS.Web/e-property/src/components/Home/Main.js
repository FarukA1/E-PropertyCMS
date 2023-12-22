import React, { useEffect, useState } from 'react';
import { clientService } from './../app-services/client-service';

const Main = () => {
  const [clients, setClients] = useState([]);

  useEffect(() => {
    const fetchClients = async () => {
      try {
        const queryParams = {
          // Add any query parameters you need here
        };

        const response = await clientService.getClients(queryParams);
        setClients(response.data);
        clients = response.data;// Assuming the response is an array of clients
      } catch (error) {
        console.error('Error fetching clients:', error);
      }
    };

    fetchClients();
  }, []); // Empty dependency array ensures the effect runs only once on mount

  return (
    <div>
      <h1>Clients</h1>
      <table>
        <thead>
          <tr>
            <th>Client ID</th>
            {/* Add other column headers as needed */}
          </tr>
        </thead>
        <tbody>
          {clients.map((client) => (
            <tr key={client.id}>
              <td>{client.id}</td>
              {/* Add other cells as needed */}
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
};

export default Main;
