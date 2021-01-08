let img = new Image();
img.src = 'Astra2.png';
img.onload = function() {
    window.requestAnimationFrame(gameLoop);
};

var i =0;
var x=10;
var y=10;


let canvas = document.querySelector('canvas');
let ctx = canvas.getContext('2d');
let Adata = document.getElementById("Adata").textContent
let Bdata = document.getElementById("Bdata").textContent
let Cdata = document.getElementById("Cdata").textContent
let ItemArray = [Adata,Bdata,Cdata]
const SCALE = 0.5;
const WIDTH = 312;
const HEIGHT = 292;
const SCALED_WIDTH = SCALE * WIDTH;
const SCALED_HEIGHT = SCALE * HEIGHT;
const ANSWER_SIZE = 120;
function drawAnswers(){
    let imageObj = new Image();
    imageObj.src = "SpaceForGame.png";
        for (i = 0; i < 4; i++) {
            ctx.drawImage(imageObj, x , y);
            ctx.font = "40pt Calibri";
            ctx.fillText(ItemArray[i],x+ 180,120);
            x = x+ 400;
        }
        x=10;
}
function drawBgImg() {
    let bgImg = new Image();
    bgImg.src = 'BusinessIntelligence.jpg';
    
    bgImg.onload = () => {
        ctx.drawImage(bgImg, 0, 0, canvas.width, canvas.height);
    }
}

function drawFrame(frameX, frameY, canvasX, canvasY) {
    if(positionX > canvas.width - SCALED_WIDTH){
        positionX = canvas.width - SCALED_WIDTH
    }
    if(positionY > canvas.height - SCALED_HEIGHT){
        positionY = canvas.height - SCALED_HEIGHT
    }
    if(positionX < 0){
        positionX = 0
    }
    if(positionY < ANSWER_SIZE + SCALED_HEIGHT){
        positionY = 850
        keyUpListener(keyPresses.w)
        window.location.reload();
        return;
    }
    ctx.drawImage(img,
        frameX * WIDTH, frameY * HEIGHT, WIDTH, HEIGHT,
        canvasX, canvasY, SCALED_WIDTH, SCALED_HEIGHT);
}

const CYCLE_LOOP = [0, 1, 0, 2];
let currentLoopIndex = 0;
let frameCount = 0;
let currentDirection = 0;
let keyPresses = {};

window.addEventListener('keydown', keyDownListener);
function keyDownListener(event) {
    keyPresses[event.key] = true;
}

window.addEventListener('keyup', keyUpListener);
function keyUpListener(event) {
    keyPresses[event.key] = false;
}

const MOVEMENT_SPEED = 5;
let positionX = 400;
let positionY = 850;


function gameLoop() {
    ctx.clearRect(500, 500, canvas.width, canvas.height);
    if (keyPresses.w) {
        positionY -= MOVEMENT_SPEED;
    } else if (keyPresses.s) {
        positionY += MOVEMENT_SPEED;
    }
    if (keyPresses.a) {
        positionX -= MOVEMENT_SPEED;
    } else if (keyPresses.d) {
        positionX += MOVEMENT_SPEED;
    }
    drawBgImg()
    drawAnswers()
    drawFrame(0, 0, positionX, positionY);
    window.requestAnimationFrame(gameLoop);
}