const dataCount = parseInt(document.getElementById("dataCount").value);

const chartData = {
    labels: ["Developers", "Designers", "Managers", "Others"],
    data: [dataCount, 26, 13, 6]
}

const applicantChart = document.querySelector(".applicant-chart");

const chartOptions = {
    responsive: true, // Allow the chart to be responsive
    maintainAspectRatio: false, // Do not maintain the aspect ratio
    borderWidth: 5,
    borderRadius: 1,
    plugins: {
        legend: {
            position: 'bottom', // Place the legend at the bottom
            align: 'center', // Align the legend items to the center
            fullWidth: true,
        }
    }
};

new Chart(applicantChart, {
    type: "doughnut",
    data: {
        labels: chartData.labels,
        datasets: [
            {
                label: " Job Preference",
                data: chartData.data,
            }
        ],
    },
    options: chartOptions,
});