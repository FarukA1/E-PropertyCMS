import Loading from "../LoadingError/Loading";
import Error from "../LoadingError/Error";

const TopTotal = ({ loading, error, data }) => {

      return (
        
        <div className="row">
          <div className="col-lg-4">
            <div className="card card-body mb-4 shadow-sm">
            {loading ? (
              <Loading />
            ) : error ? (
              <Error variant="alert-danger">{error}</Error>
            ) : (
              <article className="icontext">
                <span className="icon icon-sm rounded-circle alert-primary">
                  <i className="text-primary fas fa-user"></i>
                </span>
                <div className="text">
                  <h6 className="mb-1">Total Clients</h6>{" "}
                  <span>{data.numberofClients}</span>
                </div>
              </article>
            )}
            </div>
          </div>
          <div className="col-lg-4">
            <div className="card card-body mb-4 shadow-sm">
            {loading ? (
              <Loading />
            ) : error ? (
              <Error variant="alert-danger">{error}</Error>
            ) : (
              <article className="icontext">
                <span className="icon icon-sm rounded-circle alert-success">
                  <i className="text-success fas fa-building"></i>
                </span>
                <div className="text">
                  <h6 className="mb-1">Total Cases</h6>
                  <span>{data.numberofCases}</span>
                </div>
              </article>
            )}
            </div>
          </div>
          <div className="col-lg-4">
            <div className="card card-body mb-4 shadow-sm">
            {loading ? (
              <Loading />
            ) : error ? (
              <Error variant="alert-danger">{error}</Error>
            ) : (
              <article className="icontext">
                <span className="icon icon-sm rounded-circle alert-warning">
                  <i className="text-warning fas fa-briefcase"></i>
                </span>
                <div className="text">
                  <h6 className="mb-1">Total Properties</h6>
                  <span>{data.numberofProperties}</span>
                </div>
              </article>
            )}
            </div>
          </div>
        </div>
      );
};

export default TopTotal;