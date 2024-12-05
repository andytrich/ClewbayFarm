import React, { useEffect, useState } from "react";
import { getGapsForBlock } from "../services/api";

interface GapsListProps {
    blockId: number;
    week: number;
}

const GapsList: React.FC<GapsListProps> = ({ blockId, week }) => {
    const [gaps, setGaps] = useState<any[]>([]); // Replace `any` with proper type if needed

    useEffect(() => {
        const fetchGaps = async () => {
            const data = await getGapsForBlock(blockId, week);
            setGaps(data);
        };

        fetchGaps();
    }, [blockId, week]);

    return (
        <div>
            <h2>Gaps for Block {blockId}, Week {week}</h2>
            <ul>
                {gaps.map((gap, index) => (
                    <li key={index}>From {gap.startWeek} to {gap.endWeek}</li>
                ))}
            </ul>
        </div>
    );
};

export default GapsList;
