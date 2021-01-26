

var i =0;
var x=0;
var y=0;
var data = []//[Question1,A,B,C,A]...

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
var Answer = ""

const MOVEMENT_SPEED = 5;
let positionX = 400;
let positionY = 850;

var flag = true;
var timeplayed;

const CYCLE_LOOP = [0, 1, 0, 2];
let currentLoopIndex = 0;
let frameCount = 0;
let currentDirection = 0;
let keyPresses = {};

async function getCsvData(){
    const response = await  fetch('quizQuestionTest.csv');// get data
    const filedata = response.text()
    var splitFile = (await filedata).split("\n")// split by line
    for (let j = 0; j < splitFile.length; j++) {// each line
        var line = splitFile[j]// current line
        var splitline = line.split(",")//split line by comma
        var array = [splitline[0],splitline[1],splitline[2],splitline[3],splitline[4].replace("\n","")]
        data.push(array) // add line to array
    }
    return data
}
(async () => {
    await getCsvData()
})()

alert("pause")
    
let img = new Image();
img.src = 'Astra2-removebg-preview.png';
var time =  new Date().valueOf()
img.onload = function() {
    window.requestAnimationFrame(gameLoop);
};

function correctAnswer(){

    console.log(Answer)
    if(Answer == "A") {
        if (positionX > (canvas.width)/ 3) {
        } else {
            Points = Points + 1
        }
    }
    else if(Answer == "B") {
        if (positionX < 2 * (canvas.width / 3) || positionX > canvas.width / 3) {
            Points = Points + 1
        } else {
        }
    }
        else if(Answer == "C") {
            if (positionX < 2 * (canvas.width / 3)) {
            } else {
                Points = Points + 1
            }
        }
    }
function drawAnswers(){
         x=0;
        for (i = 1; i < 4; i++) {
            let imageObj = new Image();
            imageObj.onload = function () {
                ctx.drawImage(imageObj, x, y, canvas.width / 3, canvas.height / 5);
                ctx.strokeRect(x, y, canvas.width / 3, canvas.height / 5)
                ctx.font = "30px Comic Sans ms";
                ctx.fillStyle = "white"
                document.getElementById("question").innerHTML = data[questioncount][0]
                document.getElementById("points").innerHTML = "You have scored " + Points + "/10 points so far"
                Answer = data[questioncount][4]
                ctx.fillText(data[questioncount][(x+400)/400], x + 180, 120);
                x = x + 400;
                }
                imageObj.src = "1200px-Hyades.jpg"
            }
        x= 0;
        }
        
function drawBoxes(){
    
}
function drawBgImg() {
    let bgImg = new Image();
    bgImg.src = '1200px-Hyades.jpg';
    ctx.drawImage(bgImg, 0, 0 + ANSWER_SIZE, canvas.width, canvas.height - ANSWER_SIZE);
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
    if(positionY < ANSWER_SIZE || timeplayed > 10){
        correctAnswer()
        positionY = 850
        keyPresses=[]
        questioncount = questioncount + 1;
        flag = true;
        return true;
    }
    ctx.drawImage(img,
        frameX * WIDTH, frameY * HEIGHT, WIDTH, HEIGHT,
        canvasX, canvasY, SCALED_WIDTH, SCALED_HEIGHT);
}

window.addEventListener('keydown', keyDownListener);
function keyDownListener(event) {
    keyPresses[event.key] = true;
}

window.addEventListener('keyup', keyUpListener);
function keyUpListener(event) {
    keyPresses[event.key] = false;
}

function gameLoop() {
    timeplayed = Math.round((new Date().valueOf() - time)/1000)
    document.getElementById("time").innerHTML = timeplayed
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
        if (flag === true){
            drawAnswers()
            flag = false;
        }
            
        let won = drawFrame(0, 0, positionX, positionY);
        if (won === true || timeplayed > 10) {
            time = new Date().valueOf()
            flag = true;
            Points = Points + 1;
            window.requestAnimationFrame(gameLoop);
        } else {
            window.requestAnimationFrame(gameLoop);
        }
    }
    else {	
        let Points2 = Points.toString()
        sessionStorage.setItem("Points", Points2)
        document.cookie = "Points" + "=" + Points2 + "; " + ";localhost=;path=/";
        window.location.replace("/LeaderboardEntry");
        return;
    }
}