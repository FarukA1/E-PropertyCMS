import Loading from "../LoadingError/Loading";
import Error from "../LoadingError/Error";

const LatestCases = ({ loading, error, cases }) => {

      return (
        <div className="card mb-4 shadow-sm">
          <div className="card-body">
            <h4 className="card-title">Latest Cases</h4>
            {loading ? (
              <Loading />
            ) : error ? (
              <Error variant="alert-danger">{error}</Error>
            ) : (
              <div className="table-responsive">
                <table className="table">
                  <tbody>
                    {cases.map((kase) => (
                      <tr key={kase.id}>
                        <td>
                          <b>{kase.reference}</b>
                        </td>
                        <td>{kase.lastModifiedOn}</td>
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

export default LatestCases;