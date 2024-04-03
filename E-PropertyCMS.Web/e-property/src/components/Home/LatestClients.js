import { useNavigate } from "react-router-dom";
import Loading from "../LoadingError/Loading";
import Error from "../LoadingError/Error";

const LatestClients = ({ loading, error, clients }) => {
    const navigate = useNavigate();

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