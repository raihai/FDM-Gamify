let fileNa = document.getElementById("filename").textContent // get filename through session
window.onload  = startGame(); // startGame is loaded first

//storing separate element from each data entry
const quest = [];
const option1 = [];
const option2 =[];
const option3 = [];
const ans = [];

// function to get data from the csv file 
async function getData(){
    const response = await fetch(fileNa);
    const data = await response.text()
    const rows = data.split('\n');
    rows.forEach( element => {
        const row = element.split(',');
        quest.push(row[0]);
        option1.push(row[1]);
        option2.push(row[2]);
        option3.push(row[3]);
        ans.push(row[4]);
     
            
        }

    )
}


async function startGame() {
    await getData()
    
    
    //stores the separate elements in a random sequence
    const randQuest = [];
    const randOption1 = [];
    const randOption2 = [];
    const randOption3 = [];
    const randAns = [];

    //randomises the questions
    for (let i = 0; i < 10; i++) {
        let idx = Math.floor(Math.random() * quest.length);
        randQuest.push(quest[idx]);
        quest.splice(idx, 1);
        randOption1.push(option1[idx]);
        option1.splice(idx, 1);
        randOption2.push(option2[idx]);
        option2.splice(idx, 1);
        randOption3.push(option3[idx]);
        option3.splice(idx, 1);
        randAns.push(ans[idx]);
        ans.splice(idx, 1);
    }

    // question format
    let questions = [
        {
            numb: 1,
            questions: randQuest[0],
            answer: randAns[0],
            options: [
                randOption1[0],
                randOption2[0],
                randOption3[0]
            ]
        },
        {
            numb: 2,
            questions: randQuest[1],
            answer: randAns[1],
            options: [
                randOption1[1],
                randOption2[1],
                randOption3[1]
            ]
        },
        {
            numb: 3,
            questions: randQuest[2],
            answer: randAns[2],
            options: [
                randOption1[2],
                randOption2[2],
                randOption3[2]
            ]
        },
        {
            numb: 4,
            questions: randQuest[3],
            answer: randAns[3],
            options: [
                randOption1[3],
                randOption2[3],
                randOption3[3]
            ]
        },
        {
            numb: 5,
            questions: randQuest[4],
            answer: randAns[4],
            options: [
                randOption1[4],
                randOption2[4],
                randOption3[4]
            ]
        },
        {
            numb: 6,
            questions: randQuest[5],
            answer: randAns[5],
            options: [
                randOption1[5],
                randOption2[5],
                randOption3[5]
            ]
        },
        {
            numb: 7,
            questions: randQuest[6],
            answer: randAns[6],
            options: [
                randOption1[6],
                randOption2[6],
                randOption3[6]
            ]
        },
        {
            numb: 8,
            questions: randQuest[7],
            answer: randAns[7],
            options: [
                randOption1[7],
                randOption2[7],
                randOption3[7]
            ]
        },
        {
            numb: 9,
            questions: randQuest[8],
            answer: randAns[8],
            options: [
                randOption1[8],
                randOption2[8],
                randOption3[8]
            ]
        },
        {
            numb: 10,
            questions: randQuest[9],
            answer: randAns[9],
            options: [
                randOption1[9],
                randOption2[9],
                randOption3[9]
            ]
        },
        
    ];


//select all required elements
    const start_butn = document.querySelector(".start_btn button");
    const rule_box = document.querySelector(".rule_box");
    const exit_btn = rule_box.querySelector(".buttons .quit");
    const continue_btn = rule_box.querySelector(".buttons .restart");
    const quiz_box = document.querySelector(".quiz_box");
    const result_box = document.querySelector(".result_box");
    const option_list = document.querySelector(".option_list");
    const time_line = document.querySelector("header .time_line");
    const timeText = document.querySelector(".timer .time_left_txt");
    const timeCount = document.querySelector(".timer .timer_sec");

// when startQuiz button is clicked
    start_butn.onclick = () => {
        rule_box.classList.add("activeRule"); //display rule box
    }

// when exitQuiz button is clicked
    exit_btn.onclick = () => {
        rule_box.classList.remove("activeRule"); //display rule box
        window.location.replace("/Branch-Selection")
    }

// if continueQuiz button is clicked
    continue_btn.onclick = () => {
        rule_box.classList.remove("activeRule"); //hide rule box
        quiz_box.classList.add("activeQuiz"); //display quiz box
        
        // calls functions to start the quiz
        showQuestions(0);
        questCounter(1); 
        startTime(12); 
        startTimeLine(0); 

    }

    // quiz variables 
    
    let timeValue = 12;
    let quest_count = 0;
    let que_numb = 1;
    let userScore = 0;
    let counter;
    let counterLine;
    let widthValue = 0;

    const restart_quiz = result_box.querySelector(".buttons .restart");
    const submit_quiz = result_box.querySelector(".buttons .quit");
 
// when restartQuiz button is clicked
    
    restart_quiz.onclick = () => {
        quiz_box.classList.add("activeQuiz"); //display quiz box
        result_box.classList.remove("activeResult"); //hide result box
        timeValue = 12;
        quest_count = 0;
        que_numb = 1;
        userScore = 0;
        widthValue = 0;
        
        // clear functions to restart the quiz
        showQuestions(quest_count);
        questCounter(que_numb); 
        clearInterval(counter); 
        clearInterval(counterLine); 
        startTime(timeValue);
        startTimeLine(widthValue);
        timeText.textContent = "Time Left";
        next_btn.classList.remove("show"); 

    }

// if submit to leaderboard button is clicked
    submit_quiz.onclick = () => {
        let Points = userScore.toString()

        sessionStorage.setItem("Points", Points) // storing user score as a session
        window.location.replace("/LeaderboardEntry"); //load LeaderboardEntry Page
    }

    const next_btn = document.querySelector("footer .next_btn");
    const bottom_ques_counter = document.querySelector("footer .total_que");

// when Next Question button is clicked
    next_btn.onclick = () => {
        
        // display new questions unless question count is more than total question length
        if (quest_count < questions.length - 1) { 
            quest_count++; 
            que_numb++; 
            showQuestions(quest_count); 
            questCounter(que_numb); 
            clearInterval(counter); 
            clearInterval(counterLine); 
            startTime(timeValue); 
            startTimeLine(widthValue); 
            timeText.textContent = "Time Left"; 
            next_btn.classList.remove("show"); 
        } else { // else displays the result box
            
            clearInterval(counter); 
            clearInterval(counterLine); 
            showResultBox();
        }
    }

    // functions to get questions and options from array
    
    function showQuestions(index) {
      
        const que_text = document.querySelector(".que_text");

        //new span and div tag to store questions and options
        let que_tag = '<span>' + questions[index].numb + ". " + questions[index].questions + '</span>';
        let option_tag = '<div class="option" id="question1"><span>' + questions[index].options[0] + '</span></div>'
            + '<div class="option" id="question2"><span>' + questions[index].options[1] + '</span></div>'
            + '<div class="option " id="question3"><span>' + questions[index].options[2] + '</span></div>';
        que_text.innerHTML = que_tag; 
        option_list.innerHTML = option_tag; 

        const option = option_list.querySelectorAll(".option");
        
        
        
        // setting onclick attribute to all available options
        
        for (i = 0; i < option.length; i++) {
           
            document.getElementById(option[i].id).onclick = function(){ optionSelected(this);}
            
        }
    }

    // icon div tags
    let tickIconTag = '<div class="icon tick"><i class="fas fa-check"></i></div>';
    let crossIconTag = '<div class="icon cross"><i class="fas fa-times"></i></div>';

    
    
    // function for when the user clicks an option
    function optionSelected(answer) {

        clearInterval(counter); // clear the counter
        clearInterval(counterLine); // clear the counter line
        let userAns = answer.textContent; //getting user selected option
        console.log(userAns);
        let CorrectAns = questions[quest_count].answer.replace(/[^\x20-\x7E]/g, ''); //get correct answer and remove hidden char
        console.log(CorrectAns);
        const allOptions = option_list.children.length; //get all option items

        console.log(allOptions);
        

        console.log("corect answer: ", CorrectAns)
        console.log("user Answer: ", userAns)
        console.log("type of user Answer", typeof userAns)
        console.log("type of correct Answer", typeof CorrectAns)
        
        
        // if user answer is equal to correct answer then update the score
        if(userAns == CorrectAns){ 
            userScore += 1; 
            answer.classList.add("correct"); 
            answer.insertAdjacentHTML("beforeend", tickIconTag); 
            console.log("Correct Answer");
            console.log("Your correct answers = " + userScore);
        }else{ // else show the correct answer
            answer.classList.add("incorrect"); 
            answer.insertAdjacentHTML("beforeend", crossIconTag); 
            console.log("Wrong Answer");

            for(i=0; i < allOptions; i++){
                if(option_list.children[i].textContent == CorrectAns){ 
                    option_list.children[i].setAttribute("class", "option correct"); 
                    option_list.children[i].insertAdjacentHTML("beforeend", tickIconTag); 
                    console.log("Auto selected correct answer.");
                }
            }
        }
        for(i=0; i < allOptions; i++){
            option_list.children[i].classList.add("disabled"); //disabled all options after user has selected an option
        }
        next_btn.classList.add("show"); //display the next button after the user selects an option
    }
    function test(){
        alert("in")
    }

    
    //function to show result
    
    function showResultBox() {
        rule_box.classList.remove("activeRule"); //hide rule box
        quiz_box.classList.remove("activeQuiz"); //hide quiz box
        result_box.classList.add("activeResult"); //display result box
        const scoreText = result_box.querySelector(".score_text");
        if (userScore > 7) { // if user scored more than 7
            //create a new span tag and pass the user score number and total question number
            let scoreTag = '<span>and congrats! 🎉, You got <p>' + userScore + '</p> out of <p>' + questions.length + '</p></span>';
            scoreText.innerHTML = scoreTag;  //adding new span tag inside score_Text
        } else if (userScore > 5) { // if user scored more than 5
            let scoreTag = '<span>and nice 😎, You got <p>' + userScore + '</p> out of <p>' + questions.length + '</p></span>';
            scoreText.innerHTML = scoreTag;
        } else { // if user scored less than 5
            let scoreTag = '<span>and sorry 😐, You got only <p>' + userScore + '</p> out of <p>' + questions.length + '</p></span>';
            scoreText.innerHTML = scoreTag;
        }
    }

    // function to start timer
    
    function startTime(time) {
        counter = setInterval(timer, 1000);

        //counting down
        function timer() {
            timeCount.textContent = time; 
            time--; 
            
            // add 0 before the time value when the timer is less than 9
            if (time < 9) { 
                let addZero = timeCount.textContent;
                timeCount.textContent = "0" + addZero; 
            }
            // stop timer and quiz option functionality after the timer runs out
            if (time < 0) { 
                clearInterval(counter); 
                timeText.textContent = "Time Off";
                const allOptions = option_list.children.length; 
                let CorrectAns = questions[quest_count].answer; 
                for (i = 0; i < allOptions; i++) {
                    if (option_list.children[i].textContent == CorrectAns) { 
                        option_list.children[i].setAttribute("class", "option correct"); 
                        option_list.children[i].insertAdjacentHTML("beforeend", tickIconTag); 
                        console.log("Time Off: Auto selected correct answer.");
                    }
                }
                for (i = 0; i < allOptions; i++) {
                    option_list.children[i].classList.add("disabled"); //disabled all options after user has selected an option
                }
                next_btn.classList.add("show"); //display the next button after the user selects an option
            }
        }

    }
    
    // function to start timer line
    function startTimeLine(time) {
        counterLine = setInterval(timer, 29);
        
        //displaying the timer line
        function timer() {
            time += 1;
            time_line.style.width = time + "px";
            if (time > 549) { 
                clearInterval(counterLine);
            }
        }
    }

    // function to count question
    function questCounter(index) {
        let totalQueCounTag = '<span><p>' + index + '</p> of <p>' + questions.length + '</p> Questions</span>';
        bottom_ques_counter.innerHTML = totalQueCounTag;  
    }
}

