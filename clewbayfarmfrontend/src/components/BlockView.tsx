import React, { useEffect, useState } from "react";
import { getCropsInBlock } from "../services/api";

interface BlockCropDetails {
    bedId: number;
    cropType: string;
    cropVariety: string;
    plantingDate: string; // ISO string from API
    removalDate: string; // ISO string from API
    plantingWeek: number;
    removalWeek: number;
}

interface BlockViewProps {
    blockId: number;
    week: number;
}

const BlockView: React.FC<BlockViewProps> = ({ blockId, week }) => {
    const [crops, setCrops] = useState<BlockCropDetails[]>([]);

    useEffect(() => {
        const fetchBlockData = async () => {
            const data = await getCropsInBlock(blockId, week);
            setCrops(data);
        };

        fetchBlockData();
    }, [blockId, week]);

    return (
        <div>
            <h2>Block Crops for Week {week}</h2>
            {crops.length === 0 ? (
                <p>No crops found for this block in the given week.</p>
            ) : (
                <ul>
                    {crops.map((crop) => (
                        <li key={crop.bedId}>
                            <strong>Bed {crop.bedId}:</strong> {crop.cropType} ({crop.cropVariety}) <br />
                            Planting Date: {new Date(crop.plantingDate).toLocaleDateString()} <br />
                            Removal Date: {crop.removalDate ? new Date(crop.removalDate).toLocaleDateString() : "N/A"}
                        </li>
                    ))}
                </ul>
            )}
        </div>
    );
};

export default BlockView;
