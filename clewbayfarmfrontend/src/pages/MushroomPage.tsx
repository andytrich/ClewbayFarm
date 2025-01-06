import React, { useEffect, useState } from "react";
import { Line } from "react-chartjs-2";
import {
  Chart as ChartJS,
  CategoryScale,
  LinearScale,
  PointElement,
  LineElement,
  Title,
  Tooltip,
  Legend,
} from "chart.js";
import { getMushroomEnvironment } from "../services/api";

// Register Chart.js components
ChartJS.register(
  CategoryScale,
  LinearScale,
  PointElement,
  LineElement,
  Title,
  Tooltip,
  Legend
);

// Define the structure of the response data
interface MushroomEnvironment {
  id: number;
  temperature: number;
  humidity: number;
  cO2Level: number;
  recordedDateTime: string; // ISO date string
}

const MushroomPage: React.FC = () => {
  const [data, setData] = useState<MushroomEnvironment[]>([]);
  const [loading, setLoading] = useState<boolean>(true);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    const fetchMushroomData = async () => {
      try {
        const response = await getMushroomEnvironment();
        setData(response as MushroomEnvironment[]); // Ensure proper type casting
      } catch (err) {
        setError("Failed to fetch mushroom environment data.");
      } finally {
        setLoading(false);
      }
    };

    fetchMushroomData();
  }, []);

  if (loading) return <p>Loading...</p>;
  if (error) return <p>{error}</p>;

  // Prepare data for the chart
  const chartData = {
    labels: data.map((entry) =>
      new Date(entry.recordedDateTime).toLocaleString()
    ),
    datasets: [
      {
        label: "Temperature (°C)",
        data: data.map((entry) => entry.temperature),
        borderColor: "rgba(255, 99, 132, 1)",
        fill: false,
      },
      {
        label: "Humidity (%)",
        data: data.map((entry) => entry.humidity),
        borderColor: "rgba(54, 162, 235, 1)",
        fill: false,
      },
      {
        label: "CO2 Level (ppm)",
        data: data.map((entry) => entry.cO2Level),
        borderColor: "rgba(75, 192, 192, 1)",
        fill: false,
      },
    ],
  };

  const chartOptions = {
    responsive: true,
    plugins: {
      legend: {
        display: true,
        position: "top" as const,
      },
      title: {
        display: true,
        text: "Mushroom Environment Over Time",
      },
    },
    scales: {
      x: {
        title: {
          display: true,
          text: "Time",
        },
      },
      y: {
        title: {
          display: true,
          text: "Value",
        },
      },
    },
  };

  return (
    <div>
      <h1>Mushroom Environment Monitoring</h1>
      <Line data={chartData} options={chartOptions} />
    </div>
  );
};

export default MushroomPage;
