import * as signalR from "@microsoft/signalr";

export async function startHub() {
  const token = localStorage.getItem("accessToken");

  const connection = new signalR.HubConnectionBuilder()
    .withUrl("http://localhost:5001/hubs/notifications", {
      accessTokenFactory: () => token || "",
    })
    .withAutomaticReconnect()
    .build();

  try {
    await connection.start();
    console.log("SignalR connected");
  } catch (err) {
    console.error("SignalR connection failed:", err);
  }

  return connection;
}
