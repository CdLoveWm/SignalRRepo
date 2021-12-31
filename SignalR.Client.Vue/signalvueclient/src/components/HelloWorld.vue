<template>
  <div class="hello">
    <h1>{{ tip }}</h1>
    <div>
      <textarea id="message" v-model="newMessag"></textarea>
      </div>
      <div>
          <input type="button" value="发送" id="sendButton" @click="SendMessage">
      </div>
      <br>
      <div>收到消息：</div>
      <ul v-bind:key="index" v-for="(item,index) in messagesList">
          <li>{{ item }}</li>
      </ul>
  </div>
</template>

<script>
// const signalR = require('@microsoft/signalr')
// eslint-disable-next-line no-unused-vars
import * as signalR from '@aspnet/signalr'
export default {
  name: 'HelloWorld',
  data () {
    return {
      tip: '这是Vue项目中使用SignalR示例',
      messagesList: [],
      newMessag: '',
      connection: null
    }
  },
  mounted: function () {
    this.init()
  },
  methods: {
    // 初始化连接
    init: function () {
      this.connection = new signalR.HubConnectionBuilder()
        .withUrl('http://localhost:5000/myhub') // SignrlR服务地址
        .configureLogging(signalR.LogLevel.Information)
        .build()
      // 监听ReceiveMessage
      this.connection.on('ReceiveMessage', (user, message) => {
        const encodedMsg = `${user} says ${message}`
        this.messagesList.push(encodedMsg)
      })
      // 开启连接
      async function start () {
        try {
          await this.connection.start()
          console.log('connected')
        } catch (err) {
          console.log(err)
          setTimeout(() => start(), 5000)
        }
      };
      // 连接关闭时的事件（关闭时重新启动）
      this.connection.onclose(async () => {
        await start()
      })
      start()
    },
    // 消息发送
    SendMessage: function () {
      const user = 'js-vue client'
      // 执行SignalR服务器SendMessage方法
      this.connection.invoke('SendMessage', user, this.newMessag).catch(err => console.error(err))
    }
  }
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
h1, h2 {
  font-weight: normal;
}
ul {
  list-style-type: none;
  padding: 0;
}
li {
  display: inline-block;
  margin: 0 10px;
}
a {
  color: #42b983;
}
</style>

// npm install --save @microsoft/signalr
