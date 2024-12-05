import React, { useEffect, useState } from "react";
import { getJobsForWeek } from "../services/api";
import { Job } from "../types/Job";

interface JobsListProps {
    week: number;
    year: number;
}

const JobsList: React.FC<JobsListProps> = ({ week, year }) => {
    const [jobs, setJobs] = useState<Job[]>([]);

    useEffect(() => {
        const fetchJobs = async () => {
            const data = await getJobsForWeek(week, year);
            setJobs(data.jobs);
        };

        fetchJobs();
    }, [week, year]);

    return (
        <div>
            <h2>Jobs for Week {week}, {year}</h2>
            <ul>
                {jobs.map((job, index) => (
                    <li key={index}>
                        {job.date}: {job.action} - {job.crop} {job.bedOrTray && `(${job.bedOrTray})`}
                    </li>
                ))}
            </ul>
        </div>
    );
};

export default JobsList;
