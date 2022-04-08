import styles from './index.less';
import { useState, useEffect } from 'react';
import { Button, Input, Space } from 'antd';
import * as signalR from '@microsoft/signalr';

const connection = new signalR.HubConnectionBuilder().withUrl('http://192.168.1.2:5151/hub').build();
connection.start()
  .then(res => {
    console.log('connectiton success', res);

  })
  .catch(err => console.error(err));

export default function IndexPage() {
  const [messageLogs, setMessageLogs] = useState(['']);
  const [message, setMessage] = useState('');
  const [user, setUser] = useState('');
  const [toUser, setToUser] = useState('');


  connection.on('messageReceived', (userName: string, messageContent: string) => {
    setMessageLogs([...messageLogs, `${userName}: ${messageContent}`]);
  });

  return (
    <div style={{margin:30}}>
      <h1 className={styles.title}>Page index</h1>
      <div >
        <Space>
          <Input style={{ width: 100 }} value={user} onChange={e => setUser(e.target.value)} />
          
          <Button type="primary" onClick={() => {
            connection.invoke('AddUserName', user);

          }}>login</Button>
          <Input style={{ width: 100 }} value={toUser} onChange={e => setToUser(e.target.value)} />
        </Space>
      </div>
      <div style={{height:500,width:300,border:'1px solid black'}}>
        {
          messageLogs.map((item, index) => {
            return <p key={index}>{item}</p>
          })
        }
      </div>
      <div>
        <Space>
          <Input style={{ width: 200 }} value={message} onChange={e => setMessage(e.target.value)} />
          <Button type='primary' onClick={() => {
            setMessageLogs(messageLogs => [...messageLogs, `${user}: ${message}`]);
            connection.send('privateMessage', user, toUser, message);
          }}>send</Button>
        </Space>

      </div>
    </div>
  );
}
