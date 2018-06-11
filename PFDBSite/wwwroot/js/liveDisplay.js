// The following sample code uses modern ECMAScript 6 features 
// that aren't supported in Internet Explorer 11.
// To convert the sample for environments that do not support ECMAScript 6, 
// such as Internet Explorer 11, use a transpiler such as 
// Babel at http://babeljs.io/. 
//
// See Es5-chat.js for a Babel transpiled version of the following code:

var canvas, ctx,
  flag = false,
  prevX = 0,
  prevY = 0,
  currX = 0,
  w = 0,
  h = 0,
  currY = 0,
  dot_flag = false;

var x = "black",
  y = 2;

const connection = new signalR.HubConnectionBuilder()
  .withUrl("/liveDisplayHub")
  .build();

connection.on("ReceiveMessage", (user, message) => {
  const encodedMsg = user + ": " + msg;
  const li = document.createElement("li");
  li.textContent = encodedMsg;
  document.getElementById("messagesList").appendChild(li);
});

connection.start().catch(err => console.error(err.toString()));

document.getElementById("sendButton").addEventListener("click", event => {
  const user = document.getElementById("userInput").value;
  const message = document.getElementById("messageInput").value;
  connection.invoke("SendMessage", user, message).catch(err => console.error(err.toString()));
  event.preventDefault();
});

function canvasLoad() {
  canvas = document.getElementById("liveCanvas");
  ctx = canvas.getContext("2d");
  w = canvas.width;
  h = canvas.height;

  canvas.addEventListener("mousemove", function (e) { findxy('move', e) }, false);
  canvas.addEventListener("mousedown", function (e) { findxy('down', e) }, false);
  canvas.addEventListener("mouseup", function (e) { findxy('up', e) }, false);
  canvas.addEventListener("mouseout", function (e) { findxy('out', e) }, false);

}

function changeBgImage(imgPath) {

  var img = document.getElementById("liveMapBg");

  saveCanvas();
  clearCanvas();

  img.src = imgPath;

  ctx.drawImage(img, 0, 0);
}

function saveCanvas() {

}

function clearCanvas() {
  ctx.clearRect(0, 0, w, h);
}

function loadCanvas() {

}

function color(obj) {
  switch (obj.id) {
    case "green":
      x = "green";
      break;
    case "blue":
      x = "blue";
      break;
    case "red":
      x = "red";
      break;
    case "yellow":
      x = "yellow";
      break;
    case "orange":
      x = "orange";
      break;
    case "purple":
      x = "purple";
      break;
    case "black":
      x = "black";
      break;
    case "white":
      x = "white";
      break;
  }
}

function draw() {
  ctx.beginPath();
  ctx.moveTo(prevX, prevY);
  ctx.lineTo(currX, currY);
  ctx.strokeStyle = x;
  ctx.lineWidth = y;
  ctx.stroke();
  ctx.closePath();
}