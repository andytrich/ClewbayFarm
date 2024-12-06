import React from "react";
import { Link } from "react-router-dom";

const Navbar: React.FC = () => {
    return (
        <div className="d-flex flex-column bg-light vh-100 p-3" style={{ width: "250px" }}>
            <h2 className="text-center">Clewbay Farm</h2>
            <ul className="nav flex-column">
                <li className="nav-item">
                    <Link className="nav-link" to="/blocks">Blocks</Link>
                </li>
                <li className="nav-item">
                    <Link className="nav-link" to="/jobs">Jobs</Link>
                </li>
                <li className="nav-item">
                    <Link className="nav-link" to="/gaps">Gaps</Link>
                </li>
            </ul>
        </div>
    );
};

export default Navbar;
