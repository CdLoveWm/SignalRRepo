var a = new Vue({
    el: '#app',
    data:{
        messagesList: [],
        newMessag: '',
        connection: null
    },
    mounted: function(){
        this.init()
    },
    methods:{
        // 初始化连接
        init: function(){ 
            connection = new signalR.HubConnectionBuilder()
                .withUrl("http://localhost:5000/myhub") // SignrlR服务地址
                .configureLogging(signalR.LogLevel.Information)
                .build()
            // 监听ReceiveMessage
            connection.on("ReceiveMessage", (user, message) => {
                const encodedMsg = `${user} says ${message}`;
                this.messagesList.push(encodedMsg)
            })
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
        },
        // 消息发送
        SendMessage:function(){
            const user = "js-vue client";
            // 执行SignalR服务器SendMessage方法
            connection.invoke("SendMessage", user, this.newMessag).catch(err => console.error(err));
        }
    }
})