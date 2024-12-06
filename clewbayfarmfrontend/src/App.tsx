import React from "react";
import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import Navbar from "./components/NavBar";
import BlocksPage from "./pages/BlocksPage";
import JobsPage from "./pages/JobsPage";
import GapsPage from "./pages/GapsPage";

const App: React.FC = () => {
    return (
        <Router>
            <div className="d-flex">
                <Navbar />
                <div className="flex-grow-1 p-3">
                    <Routes>
                        <Route path="/blocks" element={<BlocksPage />} />
                        <Route path="/jobs" element={<JobsPage />} />
                        <Route path="/gaps" element={<GapsPage />} />
                    </Routes>
                </div>
            </div>
        </Router>
    );
};

export default App;
