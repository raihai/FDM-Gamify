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
const SCALE = 0.5;
const WIDTH = 312;
const HEIGHT = 292;
const SCALED_WIDTH = SCALE * WIDTH;
const SCALED_HEIGHT = SCALE * HEIGHT;
const ANSWER_SIZE = canvas.height/5;
let questioncount = 0
let Points = 0

function correctAnswer(){
    console.log(Answer)
    if(Answer == 1) {
        if (positionX > (canvas.width)/ 3) {
            alert("wrong")
        } else {
            alert(positionX)
            alert(canvas.width/3)
            alert("correct")
            Points = Points + 1
        }
    }
    else if(Answer == 2) {
        if (positionX < 2 * (canvas.width / 3) || positionX > canvas.width / 3) {
            alert("wrong")
        } else {
            Points = Points + 1
            alert("correct")
        }
    }
        else if(Answer == 3) {
            if (positionX < 2 * (canvas.width / 3)) {
                alert("wrong")
            } else {
                Points = Points + 1
                alert("correct")
            }
        }
    }
function drawAnswers(){
    getData()
    let imageObj = new Image();
    imageObj.src = "SpaceForGame.png";
        for (i = 0; i < 4; i++) {
            ctx.drawImage(imageObj, x , y, canvas.width/3, canvas.height/5);
            ctx.strokeRect(x,y,canvas.width/3,canvas.height/5)
            ctx.font = "40pt Calibri";
            document.getElementById("question").innerHTML = questions[questioncount].question
            ctx.fillText(questions[questioncount].options[i],x+ 180,120);
            x = x+ 400;
        }
        x=10;
}
function drawBgImg() {
    let bgImg = new Image();
    bgImg.src = 'BusinessIntelligence.jpg';
    ctx.drawImage(bgImg, 0, 0, canvas.width, canvas.height);
}

function drawFrame(frameX, frameY, canvasX, canvasY) {
    if(positionX > canvas.width - SCALED_WIDTH){
        positionX = canvas.width - SCALED_WIDTH
        return false;
    }
    if(positionY > canvas.height - SCALED_HEIGHT){
        positionY = canvas.height - SCALED_HEIGHT
        return false;
    }
    if(positionX < 0){
        positionX = 0
        return false;
    }
    if(positionY < ANSWER_SIZE){
        correctAnswer()
        positionY = 850
        keyUpListener(keyPresses.w)
        keyPresses=[]
        alert(questioncount)
        questioncount = questioncount + 1;
        return true;
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
    if(questioncount < 4) {
        drawBgImg()
        drawAnswers()
        let won = drawFrame(0, 0, positionX, positionY);
        if (won === true) {
            Points = Points + 1;
            window.requestAnimationFrame(gameLoop);
        } else {
            window.requestAnimationFrame(gameLoop);
        }
    }
    else {	
        let Points2 = Points.toString()
        alert(Points2)
        sessionStorage.setItem("Points", Points2)
        document.cookie = "Points" + "=" + Points2 + "; " + ";localhost=;path=/";
        window.location.replace("/LeaderboardEntry");
        return;
    }
}