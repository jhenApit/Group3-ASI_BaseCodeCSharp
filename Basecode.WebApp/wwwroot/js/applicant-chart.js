const ReceivedElement = document.getElementById("Received");
const Received = ReceivedElement ? parseInt(ReceivedElement.value) : 0;
const DeployedElement = document.getElementById("Deployed");
const Deployed = DeployedElement ? parseInt(DeployedElement.value) : 0;
const OnboardingElement = document.getElementById("Onboarding");
const Onboarding = OnboardingElement ? parseInt(OnboardingElement.value) : 0;
const ConfirmedElement = document.getElementById("Confirmed");
const Confirmed = ConfirmedElement ? parseInt(ConfirmedElement.value) : 0;
const NotConfirmedElement = document.getElementById("NotConfirmed");
const NotConfirmed = NotConfirmedElement ? parseInt(NotConfirmedElement.value) : 0;
const UndergoingJobOfferElement = document.getElementById("UndergoingJobOffer");
const UndergoingJobOffer = UndergoingJobOfferElement ? parseInt(UndergoingJobOfferElement.value) : 0;
const ForFinalInterviewElement = document.getElementById("ForFinalInterview");
const ForFinalInterview = ForFinalInterviewElement ? parseInt(ForFinalInterviewElement.value) : 0;
const UndergoingBackgroundCheckElement = document.getElementById("UndergoingBackgroundCheck");
const UndergoingBackgroundCheck = UndergoingBackgroundCheckElement ? parseInt(UndergoingBackgroundCheckElement.value) : 0;
const ForTechnicalExamElement = document.getElementById("ForTechnicalExam");
const ForTechnicalExam = ForTechnicalExamElement ? parseInt(ForTechnicalExamElement.value) : 0;
const ForTechnicalInterviewElement = document.getElementById("ForTechnicalInterview");
const ForTechnicalInterview = ForTechnicalInterviewElement ? parseInt(ForTechnicalInterviewElement.value) : 0;
const ForHRInterviewElement = document.getElementById("ForHRInterview");
const ForHRInterview = ForHRInterviewElement ? parseInt(ForHRInterviewElement.value) : 0;
const ForScreeningElement = document.getElementById("ForScreening");
const ForScreening = ForScreeningElement ? parseInt(ForScreeningElement.value) : 0;
const ShortlistedElement = document.getElementById("Shortlisted");
const Shortlisted = ShortlistedElement ? parseInt(ShortlistedElement.value) : 0;
const RejectedElement = document.getElementById("Rejected");
const Rejected = RejectedElement ? parseInt(RejectedElement.value) : 0;

const chartData = {
    labels: [
        "Received",
        "Deployed",
        "Onboarding",
        "Confirmed",
        "NotConfirmed",
        "UndergoingJobOffer",
        "ForFinalInterview",
        "UndergoingBackgroundCheck",
        "ForTechnicalExam",
        "ForTechnicalInterview",
        "ForHRInterview",
        "ForScreening",
        "Shortlisted",
        "Rejected"
    ],
    data: [
        Received,
        Deployed,
        Onboarding,
        Confirmed,
        NotConfirmed,
        UndergoingJobOffer,
        //ForFinalInterview,
        //UndergoingBackgroundCheck,
        //ForTechnicalExam,
        //ForTechnicalInterview,
        //ForHRInterview,
        //ForScreening,
        //Shortlisted,
        //Rejected
    ]
}

const applicantChart = document.querySelector(".applicant-chart");

const chartOptions = {
    responsive: true, // Allow the chart to be responsive
    maintainAspectRatio: false, // Do not maintain the aspect ratio
    borderWidth: 5,
    borderRadius: 1,
    plugins: {
        legend: {
            display: false
        }
    }
};

new Chart(applicantChart, {
    type: "doughnut",
    data: {
        labels: chartData.labels,
        datasets: [
            {
                label: "Status",
                data: chartData.data,
            }
        ],
    },
    options: chartOptions,
});