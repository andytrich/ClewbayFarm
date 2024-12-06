import React, { useEffect, useState } from "react";
import { getAllBlocks, getCropsInBlock } from "../services/api";
import TabbedView from "../components/TabbedView";
import BlockView from "../components/BlockView";

const BlocksPage: React.FC = () => {
    const [blocks, setBlocks] = useState<any[]>([]);
    const [year, setYear] = useState<number>(2024); // Default year
    const [week, setWeek] = useState<number>(1); // Default year

    useEffect(() => {
        // Fetch block data
        const fetchData = async () => {
            const data = await getAllBlocks(); // Replace with actual API
            setBlocks(data);
        };
        fetchData();
    }, []);

    const tabs = blocks.map((block) => block.name);
    const content = blocks.map((block) => <BlockView blockId={block.blockId} week={week}></BlockView>);
    return (
        <div>
            <h1>Blocks</h1>
            <div>
            <label>
                        Week:
                        <input
                            type="number"
                            value={week}
                            onChange={(e) => setWeek(Number(e.target.value))}
                        />
                    </label>
            </div>
            <TabbedView tabs={tabs} content={content} />
        </div>
    );
};

export default BlocksPage;
