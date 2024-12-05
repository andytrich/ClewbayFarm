import React, { useState } from "react";
import JobsList from "./components/JobsList";
import GapsList from "./components/GapsList";
import BlockView from "./components/BlockView";

const App: React.FC = () => {
    const [week, setWeek] = useState<number>(10); // Default week
    const [year, setYear] = useState<number>(2024); // Default year
    const [blockId, setBlockId] = useState<number>(1); // Default block

    return (
        <div>
            <header>
                <h1>Farm Management System</h1>
                <div>
                    <label>
                        Year:
                        <input
                            type="number"
                            value={year}
                            onChange={(e) => setYear(Number(e.target.value))}
                        />
                    </label>
                    <label>
                        Week:
                        <input
                            type="number"
                            value={week}
                            onChange={(e) => setWeek(Number(e.target.value))}
                        />
                    </label>
                </div>
            </header>

            <main>
                <section>
                    <JobsList week={week} year={year} />
                </section>
                <section>
                    <GapsList blockId={blockId} week={week} />
                </section>
                <section>
                    <BlockView blockId={blockId} week={week} />
                </section>
            </main>
        </div>
    );
};

export default App;
