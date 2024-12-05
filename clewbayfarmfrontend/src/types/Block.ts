import { Bed } from "./Bed";

export interface Block {
    blockId: number;
    name: string;
    type: string; // e.g., "Outdoor" or "Polytunnel"
    beds: Bed[];
}
