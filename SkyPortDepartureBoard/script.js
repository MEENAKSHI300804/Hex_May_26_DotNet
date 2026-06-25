const board = document.getElementById("board");
const clock = document.getElementById("clock");
const dateBox = document.getElementById("date");
const summary = document.getElementById("summary");

const addBtn = document.getElementById("addBtn");
const resetBtn = document.getElementById("resetBtn");
const customBtn = document.getElementById("customBtn");
const formBox = document.getElementById("formBox");
const submitBtn = document.getElementById("submitBtn");

const initialFlights = [
  { time: "06:20", flight: "AI 101", destination: "Delhi", gate: "A1", status: "DEPARTED" },
  { time: "07:10", flight: "EK 502", destination: "Dubai", gate: "B3", status: "BOARDING" },
  { time: "08:45", flight: "BA 210", destination: "London", gate: "C2", status: "ON TIME" },
  { time: "09:30", flight: "SQ 331", destination: "Singapore", gate: "D5", status: "DELAYED" },
  { time: "10:15", flight: "QR 740", destination: "Doha", gate: "A7", status: "ON TIME" },
  { time: "11:50", flight: "LH 900", destination: "Frankfurt", gate: "E4", status: "ON TIME" }
];

const extraFlights = [
  { time: "12:30", flight: "AF 118", destination: "Paris", gate: "F1", status: "ON TIME" },
  { time: "13:20", flight: "TK 405", destination: "Istanbul", gate: "B9", status: "ON TIME" },
  { time: "14:40", flight: "UA 220", destination: "New York", gate: "C8", status: "BOARDING" },
  { time: "16:05", flight: "JL 707", destination: "Tokyo", gate: "D2", status: "ON TIME" }
];

let flights = [...initialFlights];
let extraIndex = 0;

function updateClock() {
  const now = new Date();

  clock.textContent = now.toLocaleTimeString();

  dateBox.textContent = now.toDateString();
}

setInterval(updateClock, 1000);
updateClock();

function getStatusClass(status) {
  if (status === "ON TIME") return "on-time";
  if (status === "BOARDING") return "boarding";
  if (status === "DELAYED") return "delayed";
  if (status === "GATE CLOSED") return "closed";
  if (status === "DEPARTED") return "departed";
  return "on-time";
}

function renderSummary() {
  const total = flights.length;
  const boarding = flights.filter(f => f.status === "BOARDING").length;
  const delayed = flights.filter(f => f.status === "DELAYED").length;
  const departed = flights.filter(f => f.status === "DEPARTED").length;

  summary.innerHTML = `
    <div class="pill">${total} Flights</div>
    <div class="pill">${boarding} Boarding</div>
    <div class="pill">${delayed} Delayed</div>
    <div class="pill">${departed} Departed</div>
  `;
}

function renderBoard() {
  board.innerHTML = "";

  flights.sort((a, b) => a.time.localeCompare(b.time));

  flights.forEach(f => {
    const row = document.createElement("div");
    row.className = "flight-row";

    row.innerHTML = `
      <span class="time">${f.time}</span>
      <span class="flight">${f.flight}</span>
      <span class="destination">${f.destination}</span>
      <span class="gate">${f.gate}</span>
      <span class="status ${getStatusClass(f.status)}">${f.status}</span>
    `;

    board.appendChild(row);
  });

  renderSummary();
}

addBtn.addEventListener("click", () => {
  const flight = extraFlights[extraIndex % extraFlights.length];
  flights.push({ ...flight });
  extraIndex++;
  renderBoard();
});

resetBtn.addEventListener("click", () => {
  flights = [...initialFlights];
  extraIndex = 0;
  formBox.hidden = true;
  customBtn.textContent = "Custom Flight";
  renderBoard();
});

customBtn.addEventListener("click", () => {
  formBox.hidden = !formBox.hidden;
  customBtn.textContent = formBox.hidden ? "Custom Flight" : "Close Form";
});

submitBtn.addEventListener("click", () => {
  const time = document.getElementById("timeInput").value || "00:00";
  const flight = document.getElementById("flightInput").value || "XX 000";
  const destination = document.getElementById("destInput").value || "Unknown";
  const gate = document.getElementById("gateInput").value || "--";

  flights.push({
    time,
    flight: flight.toUpperCase(),
    destination,
    gate: gate.toUpperCase(),
    status: "ON TIME"
  });

  document.getElementById("timeInput").value = "";
  document.getElementById("flightInput").value = "";
  document.getElementById("destInput").value = "";
  document.getElementById("gateInput").value = "";

  formBox.hidden = true;
  customBtn.textContent = "Custom Flight";

  renderBoard();
});

setInterval(() => {
  const activeFlights = flights.filter(f => f.status !== "DEPARTED");

  if (activeFlights.length === 0) return;

  const randomFlight = activeFlights[Math.floor(Math.random() * activeFlights.length)];

  if (randomFlight.status === "ON TIME") {
    randomFlight.status = "BOARDING";
  } else if (randomFlight.status === "BOARDING") {
    randomFlight.status = "GATE CLOSED";
  } else if (randomFlight.status === "GATE CLOSED") {
    randomFlight.status = "DEPARTED";
  }

  renderBoard();
}, 5000);

renderBoard();