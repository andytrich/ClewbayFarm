import axios from "axios";

const BASE_URL = "https://localhost:7237/api"; // Replace with your backend URL

// Fetch jobs for a given week
export const getJobsForWeek = async (week: number, year: number) => {
    const response = await axios.get(`${BASE_URL}/jobs`, { params: { week, year } });
    return response.data;
};

// Fetch gaps in bed usage
export const getGapsForBlock = async (blockId: number, week: number) => {
    const response = await axios.get(`${BASE_URL}/blocks/${blockId}/gaps`, { params: { week } });
    return response.data;
};

// Fetch beds and crops for a block
export const getCropsInBlock = async (blockId: number, week: number) => {
    const response = await axios.get(`${BASE_URL}/blocks/${blockId}/crops`, { params: { week } });
    return response.data;
};