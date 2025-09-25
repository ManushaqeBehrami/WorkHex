import * as signalR from "@microsoft/signalr";

export async function startHub() {
  const token = localStorage.getItem("accessToken") || "";
  const connection = new signalR.HubConnectionBuilder()
    .withUrl("https://localhost:5001/hubs/notifications", { accessTokenFactory: () => token })
    .withAutomaticReconnect()
    .build();

  connection.on("users:registered", (payload) => console.log("New user registered:", payload));
  connection.on("app:announcement", (payload) => console.log("Announcement:", payload));

  await connection.start();
  return connection;
}
