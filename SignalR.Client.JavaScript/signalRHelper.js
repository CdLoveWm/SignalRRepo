// 创建连接
const connection = new signalR.HubConnectionBuilder()
    .withUrl("http://localhost:5000/myhub") // SignrlR服务地址
    .configureLogging(signalR.LogLevel.Information)
    .build();
// 监听ReceiveMessage
connection.on("ReceiveMessage", (user, message) => {
    const encodedMsg = `${user} says ${message}`;
    const li = document.createElement("li");
    li.textContent = encodedMsg;
    document.getElementById("messagesList").appendChild(li);
});
// 信息发送
document.getElementById("sendButton").addEventListener("click", event => {
    const user = "javascript client";
    const message = document.getElementById("message").value;
    // 执行SignalR服务器SendMessage方法
    connection.invoke("SendMessage", user, message).catch(err => console.error(err));
    event.preventDefault();
});
// 开启连接
async function start() {
    try {
        await connection.start();
        console.log("connected");
    } catch (err) {
        console.log(err);
        setTimeout(() => start(), 5000);
    }
};
// 连接关闭时的事件（关闭时重新启动）
connection.onclose(async () => {
    await start();
});

start();
