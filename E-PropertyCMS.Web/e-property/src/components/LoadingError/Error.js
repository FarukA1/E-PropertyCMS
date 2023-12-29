import React from "react";

const Error = ({ variant, children }) => {
  return (
    <div className="d-flex justify-content-center col-12">
      <div className={`alert ${variant}`}>{children}</div>
    </div>
  );
};

Error.defaultProps = {
  variant: "alert-info",
};

export default Error;
