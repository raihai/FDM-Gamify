
let fileNa = document.getElementById("filename").textContent

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
var Answer = "test"
let img;
let time;
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
    const response = await  fetch(fileNa);// get data
    const filedata = response.text()
    var splitFile = (await filedata).split("\n")// split by line
    for (let j = 0; j < splitFile.length; j++) {// each line

        var line = splitFile[j]// current line
        if (line.includes(",")) {
        var splitline = line.split(",")//split line by comma
        var array = [splitline[0], splitline[1], splitline[2], splitline[3], splitline[4]]
        data.push(array) // add line to array
        }
    }
    return data
}
(async () => {
    await getCsvData()
})()


function startgame() {
    img = new Image();
    img.src = 'Astra2-removebg-preview.png';
    time = new Date().valueOf()
    img.onload = function () {
        window.requestAnimationFrame(gameLoop);
    };
}


function correctAnswer(){




    
    if(Answer === "A") {

        if (positionX > (canvas.width)/ 3) {
            // they are in B or C
        } else {
            Points = Points + 1
        }
    }
    else if(Answer === "B") {

        if (positionX < 2 * (canvas.width / 3) || positionX > canvas.width / 3) {
            Points = Points + 1
        } else {
        }
    }
        else if(Answer === "C") {

            if (positionX < 2 * (canvas.width / 3)) {
            } else {
                Points = Points + 1
            }
        }
    }
function drawAnswers(randomquestionchooser){
         x=0;
        for (i = 1; i < 4; i++) {
            let imageObj = new Image();
            imageObj.onload = function () {
                ctx.drawImage(imageObj, x, y, canvas.width / 3, canvas.height / 5);
                ctx.strokeRect(x, y, canvas.width / 3, canvas.height / 5)
                ctx.font = "20px Comic Sans ms";
                ctx.fillStyle = "white"

                document.getElementById("question").innerHTML = data[randomquestionchooser][0]
                document.getElementById("points").innerHTML = "You have scored " + Points.toString() + "/10 points so far"

                text = data[randomquestionchooser][(x+400)/400]
                wrapText(ctx,text,x,ANSWER_SIZE/2,canvas.width/3,20)
                x = x + 400;
                if(x==1200){
                    data.splice(randomquestionchooser,1)
                }
                }
                imageObj.src = "1200px-Hyades.jpg"
            }
        x= 0;
        }

function wrapText(context, text, x, y, maxWidth, lineHeight) {
    var words = text.split(' ');
    var line = '';

    for(var n = 0; n < words.length; n++) {
        var testLine = line + words[n] + ' ';
        var metrics = context.measureText(testLine);
        var testWidth = metrics.width;
        if (testWidth > maxWidth && n > 0) {
            context.fillText(line, x, y);
            line = words[n] + ' ';
            y += lineHeight;
        }
        else {
            line = testLine;
        }

    }
    context.fillText(line, x, y);
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
    if(positionY < ANSWER_SIZE){
        correctAnswer()
        positionY = 850
        keyPresses=[]
        questioncount = questioncount + 1;
        flag = true;
        return true;
    }
    if(timeplayed > 10){
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
    timeplayed = Math.round((new Date().valueOf() - time)/1000).toString()
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
    if(questioncount < 10) {
        drawBgImg()
        if (flag === true){
            var randomquestionchooser = Math.floor(Math.random() * (10-questioncount));
            drawAnswers(randomquestionchooser)
            if(data[randomquestionchooser][1] === data[randomquestionchooser][4].slice(0, -1)){
                Answer ="A"
            }
            if(data[randomquestionchooser][2] === data[randomquestionchooser][4].slice(0, -1)){
                Answer="B"
            }
            if(data[randomquestionchooser][3] === data[randomquestionchooser][4].slice(0, -1)){
                Answer="C"
            }
            flag = false;

        }
            
        let won = drawFrame(0, 0, positionX, positionY);
        if (won === true || timeplayed > 10) {
            time = new Date().valueOf()
            flag = true;
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
        document.cookie = "PointsScored="+Points2;
        document.cookie = "QuizComplete="+fileNa.slice(0,-4)+ "; " + ";localhost=;path=/";
        return;
    }
}