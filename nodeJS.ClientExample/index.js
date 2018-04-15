const net = require('net')
const es = require('event-stream')

const client = new net.Socket()

let res = () => {}
let rej = () => {}

client
  .pipe(es.split())
  .pipe(es.map(function (data, cb) {
    res(JSON.parse(data))
    cb(null)
  }))

client.on('error', function(data) {
  console.log('ERROR: ' + data)
  rej(data)
})

client.on('close', function() {
  console.log('Connection closed')
  rej('CONNECTION CLOSED')
})

client.connect(process.env.PORT || 8000, process.env.HOST || '127.0.0.1', async function() {
  console.log('Connected')
  const s = server(client)
  const loginResponse = await s.login(process.env.LOGIN)
  console.log('logged in', loginResponse)
  const joined = await s.join()
  console.log('joined', joined)
  while(true) {
    const view = await s.getView()
    console.log('view', view)
    const response = await s.move(random(-255,255), random(-255, 255))
    console.log('move response', response)
    await wait(2000)
  }
})

function server(client) {
  const send = command => {
    client.write(JSON.stringify(command))
    client.write('\n')
    return new Promise((resolve, reject) => {
      res = resolve
      rej = reject
    })
  }

  return {
    login: username => send({Login: username, Password: 'lol'}),
    join: playerId => send({Type: 'Join', PlayerId: playerId}),
    getView: () => send({Type: 'GetView'}),
    move: (dx, dy) => send({Type: 'Move', Dx: dx, Dy: dy}),
    split: () => send({Type: 'Split'}),
    ejectMass: () => send({Type: 'EjectMass'})
  }
}

async function wait(howLong) {
  return new Promise(resolve => setTimeout(resolve, howLong))
}

function random(min, max) {
  return Math.floor(Math.random() * (max - min + 1)) + min
}
