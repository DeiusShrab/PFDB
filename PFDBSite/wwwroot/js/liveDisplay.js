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

connection.on("ReceiveDrawing", (user, drawObj) => {

  ctx.beginPath();
  ctx.moveTo(drawObj.prevX, drawObj.prevY);
  ctx.lineTo(drawObj.currX, drawObj.currY);
  ctx.strokeStyle = drawObj.x;
  ctx.lineWidth = drawObj.y;
  ctx.stroke();
  ctx.closePath();
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
    case "white":
      x = "white";
      break;

    default:
      x = "black";
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

  var drawObj;
  drawObj.prevX = prevX;
  drawObj.prevY = prevY;
  drawObj.currX = currX;
  drawObj.currY = currY;
  drawObj.x = x;
  drawObj.y = y;

  const user = document.getElementById("userInput").value;
  connection.invoke("SendDrawing", user, drawObj).catch(err => console.error(err.toString()));
}

function save() {
  document.getElementById("canvasimg").style.border = "2px solid";
  var dataURL = canvas.toDataURL();
  document.getElementById("canvasimg").src = dataURL;
  document.getElementById("canvasimg").style.display = "inline";
}

function findxy(res, e) {
  if (res == 'down') {
    prevX = currX;
    prevY = currY;
    currX = e.clientX - canvas.offsetLeft;
    currY = e.clientY - canvas.offsetTop;

    flag = true;
    dot_flag = true;
    if (dot_flag) {
      ctx.beginPath();
      ctx.fillStyle = x;
      ctx.fillRect(currX, currY, 2, 2);
      ctx.closePath();
      dot_flag = false;
    }
  }
  if (res == 'up' || res == "out") {
    flag = false;
  }
  if (res == 'move') {
    if (flag) {
      prevX = currX;
      prevY = currY;
      currX = e.clientX - canvas.offsetLeft;
      currY = e.clientY - canvas.offsetTop;
      draw();
    }
  }
}