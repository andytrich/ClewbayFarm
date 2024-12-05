import React, { useEffect, useState } from "react";
import { getCropsInBlock } from "../services/api";
import { Block } from "../types/Block";

interface BlockViewProps {
    blockId: number;
    week: number;
}

const BlockView: React.FC<BlockViewProps> = ({ blockId, week }) => {
    const [block, setBlock] = useState<Block | null>(null);

    useEffect(() => {
        const fetchBlockData = async () => {
            const data = await getCropsInBlock(blockId, week);
            setBlock(data);
        };

        fetchBlockData();
    }, [blockId, week]);

    return (
        <div>
            <h2>Block {block?.name} ({block?.type})</h2>
            {block?.beds.map((bed) => (
                <div key={bed.bedId} onClick={() => alert(`Crop: ${bed.cropType} - ${bed.cropVariety}`)}>
                    <p>Bed {bed.bedId}: {bed.cropType} ({bed.cropVariety})</p>
                </div>
            ))}
        </div>
    );
};

export default BlockView;
