export interface Job {
    action: string;
    crop: string;
    bedOrTray: string | null; // Null for propagation tasks
    date: string; // ISO string
}
