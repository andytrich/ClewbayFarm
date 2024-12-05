export interface Bed {
    bedId: number;
    cropType: string;
    cropVariety: string;
    plantingDate: string; // ISO string
    removalDate: string | null; // ISO string or null
    plantingWeek: number;
    removalWeek: number | null;
}
