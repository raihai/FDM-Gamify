window.onload  = startGame();
const quest = [];
const option1 = [];
const option2 =[];
const option3 = [];
const ans = [];




async function getData(){
    const response = await  fetch('TechnicalOperation.csv');
    const data = await response.text()
    const rows = data.split('\n');
    rows.forEach( element => {
        const row = element.split(',');
        quest.push(row[0]);
        console.log(row[0]);
        option1.push(row[1]);
        option2.push(row[2]);
        option3.push(row[3]);
        ans.push(row[4]);

            
        }

    )
}




async function startGame() {
    await getData()
    
    alert("started game")
    
    //randomises the questions
    const randQuest = [];
    const randOption1 = [];
    const randOption2 = [];
    const randOption3 = [];
    const randAns = [];
    
    
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


//selecting all required elements
    const start_btn = document.querySelector(".start_btn button");
    const rule_box = document.querySelector(".rule_box");
    const exit_btn = rule_box.querySelector(".buttons .quit");
    const continue_btn = rule_box.querySelector(".buttons .restart");
    const quiz_box = document.querySelector(".quiz_box");
    const result_box = document.querySelector(".result_box");
    const option_list = document.querySelector(".option_list");
    const time_line = document.querySelector("header .time_line");
    const timeText = document.querySelector(".timer .time_left_txt");
    const timeCount = document.querySelector(".timer .timer_sec");

// if startQuiz button clicked
    start_btn.onclick = () => {
        rule_box.classList.add("activeRule"); //show rule box
    }

// if exitQuiz button clicked
    exit_btn.onclick = () => {
        rule_box.classList.remove("activeRule"); //hide rule box
    }

// if continueQuiz button clicked
    continue_btn.onclick = () => {
        rule_box.classList.remove("activeRule"); //hide rule box
        quiz_box.classList.add("activeQuiz"); //show quiz box
        showQuetions(0); //calling showQestions function
        queCounter(1); //passing 1 parameter to queCounter
        startTimer(12); //calling startTimer function
        startTimerLine(0); //calling startTimerLine function#

    }

    let timeValue = 12;
    let que_count = 0;
    let que_numb = 1;
    let userScore = 0;
    let counter;
    let counterLine;
    let widthValue = 0;

    const restart_quiz = result_box.querySelector(".buttons .restart");
    const quit_quiz = result_box.querySelector(".buttons .quit");

// if restartQuiz button clicked
    restart_quiz.onclick = () => {
        quiz_box.classList.add("activeQuiz"); //show quiz box
        result_box.classList.remove("activeResult"); //hide result box
        timeValue = 12;
        que_count = 0;
        que_numb = 1;
        userScore = 0;
        widthValue = 0;
        showQuetions(que_count); //calling showQestions function

        queCounter(que_numb); //passing que_numb value to queCounter
        clearInterval(counter); //clear counter
        clearInterval(counterLine); //clear counterLine
        startTimer(timeValue); //calling startTimer function

        startTimerLine(widthValue); //calling startTimerLine function

        timeText.textContent = "Time Left"; //change the text of timeText to Time Left

        next_btn.classList.remove("show"); //hide the next button

    }

// if quitQuiz button clicked
    quit_quiz.onclick = () => {
        window.location.reload(); //reload the current window
    }

    const next_btn = document.querySelector("footer .next_btn");
    const bottom_ques_counter = document.querySelector("footer .total_que");

// if Next Que button clicked
    next_btn.onclick = () => {
        if (que_count < questions.length - 1) { //if question count is less than total question length
            que_count++; //increment the que_count value
            que_numb++; //increment the que_numb value
            showQuetions(que_count); //calling showQestions function
            queCounter(que_numb); //passing que_numb value to queCounter
            clearInterval(counter); //clear counter
            clearInterval(counterLine); //clear counterLine
            startTimer(timeValue); //calling startTimer function
            startTimerLine(widthValue); //calling startTimerLine function
            timeText.textContent = "Time Left"; //change the timeText to Time Left
            next_btn.classList.remove("show"); //hide the next button
        } else {
            clearInterval(counter); //clear counter
            clearInterval(counterLine); //clear counterLine
            showResult(); //calling showResult function
        }
    }

// getting questions and options from array
    function showQuetions(index) {
      
        const que_text = document.querySelector(".que_text");

        //creating a new span and div tag for question and option and passing the value using array index
        let que_tag = '<span>' + questions[index].numb + ". " + questions[index].questions + '</span>';
        let option_tag = '<div class="option" id="question1"><span>' + questions[index].options[0] + '</span></div>'
            + '<div class="option" id="question2"><span>' + questions[index].options[1] + '</span></div>'
            + '<div class="option " id="question3"><span>' + questions[index].options[2] + '</span></div>';
        que_text.innerHTML = que_tag; //adding new span tag inside que_tag
        option_list.innerHTML = option_tag; //adding new div tag inside option_tag

        const option = option_list.querySelectorAll(".option");
        
        
        
        // set onclick attribute to all available options
        for (i = 0; i < option.length; i++) {
           
            document.getElementById(option[i].id).onclick = function(){ optionSelected(this);}
            
            //document.getElementById(option[i].id).onclick = function(){ optionSelected(document.getElementById("question"+i));}
        }
    }

// creating the new div tags which for icons
    let tickIconTag = '<div class="icon tick"><i class="fas fa-check"></i></div>';
    let crossIconTag = '<div class="icon cross"><i class="fas fa-times"></i></div>';

    
    
    //if user clicked on option
    function optionSelected(answer) {

        clearInterval(counter); //clear counter
        clearInterval(counterLine); //clear counterLine
        let userAns = answer.textContent; //getting user selected option
        console.log(userAns);
        let correcAns = questions[que_count].answer.replace(/[^\x20-\x7E]/g, ''); //getting correct answer from array
        console.log(correcAns);
        const allOptions = option_list.children.length; //getting all option items

        console.log(allOptions);
        
        
        console.log(correcAns.charCodeAt(0))
        console.log(correcAns.charCodeAt(1))
        console.log(correcAns.charCodeAt(2))
        console.log(correcAns.charCodeAt(3))
        console.log(correcAns.charCodeAt(4))
        console.log(correcAns.charCodeAt(5))
        console.log(correcAns.charCodeAt(6))
        
        console.log(userAns.charCodeAt(0))
        console.log(userAns.charCodeAt(1))
        console.log(userAns.charCodeAt(2))
        console.log(userAns.charCodeAt(3))
        console.log(userAns.charCodeAt(4))
        console.log(userAns.charCodeAt(5))

        console.log("corect answer: ", correcAns)
        console.log("user Answer: ", userAns)
        console.log("type of user Answer", typeof userAns)
        console.log("type of correct Answer", typeof correcAns)
        
        

        if(userAns == correcAns){ //if user selected option is equal to array's correct answer
            userScore += 1; //upgrading score value with 1
            answer.classList.add("correct"); //adding green color to correct selected option
            answer.insertAdjacentHTML("beforeend", tickIconTag); //adding tick icon to correct selected option
            console.log("Correct Answer");
            console.log("Your correct answers = " + userScore);
        }else{
            answer.classList.add("incorrect"); //adding red color to correct selected option
            answer.insertAdjacentHTML("beforeend", crossIconTag); //adding cross icon to correct selected option
            console.log("Wrong Answer");

            for(i=0; i < allOptions; i++){
                if(option_list.children[i].textContent == correcAns){ //if there is an option which is matched to an array answer 
                    option_list.children[i].setAttribute("class", "option correct"); //adding green color to matched option
                    option_list.children[i].insertAdjacentHTML("beforeend", tickIconTag); //adding tick icon to matched option
                    console.log("Auto selected correct answer.");
                }
            }
        }
        for(i=0; i < allOptions; i++){
            option_list.children[i].classList.add("disabled"); //once user select an option then disabled all options
        }
        next_btn.classList.add("show"); //show the next button if user selected any option
    }
    function test(){
        alert("in")
    }


    function showResult() {
        rule_box.classList.remove("activeRule"); //hide rule box
        quiz_box.classList.remove("activeQuiz"); //hide quiz box
        result_box.classList.add("activeResult"); //show result box
        const scoreText = result_box.querySelector(".score_text");
        if (userScore > 3) { // if user scored more than 3
            //creating a new span tag and passing the user score number and total question number
            let scoreTag = '<span>and congrats! 🎉, You got <p>' + userScore + '</p> out of <p>' + questions.length + '</p></span>';
            scoreText.innerHTML = scoreTag;  //adding new span tag inside score_Text
        } else if (userScore > 1) { // if user scored more than 1
            let scoreTag = '<span>and nice 😎, You got <p>' + userScore + '</p> out of <p>' + questions.length + '</p></span>';
            scoreText.innerHTML = scoreTag;
        } else { // if user scored less than 1
            let scoreTag = '<span>and sorry 😐, You got only <p>' + userScore + '</p> out of <p>' + questions.length + '</p></span>';
            scoreText.innerHTML = scoreTag;
        }
    }

    function startTimer(time) {
        counter = setInterval(timer, 1000);

        function timer() {
            timeCount.textContent = time; //changing the value of timeCount with time value
            time--; //decrement the time value
            if (time < 9) { //if timer is less than 9
                let addZero = timeCount.textContent;
                timeCount.textContent = "0" + addZero; //add a 0 before time value
            }
            if (time < 0) { //if timer is less than 0
                clearInterval(counter); //clear counter
                timeText.textContent = "Time Off"; //change the time text to time off
                const allOptions = option_list.children.length; //getting all option items
                let correcAns = questions[que_count].answer; //getting correct answer from array
                for (i = 0; i < allOptions; i++) {
                    if (option_list.children[i].textContent == correcAns) { //if there is an option which is matched to an array answer
                        option_list.children[i].setAttribute("class", "option correct"); //adding green color to matched option
                        option_list.children[i].insertAdjacentHTML("beforeend", tickIconTag); //adding tick icon to matched option
                        console.log("Time Off: Auto selected correct answer.");
                    }
                }
                for (i = 0; i < allOptions; i++) {
                    option_list.children[i].classList.add("disabled"); //once user select an option then disabled all options
                }
                next_btn.classList.add("show"); //show the next button if user selected any option
            }
        }

    }

    function startTimerLine(time) {
        counterLine = setInterval(timer, 29);

        function timer() {
            time += 1; //upgrading time value with 1
            time_line.style.width = time + "px"; //increasing width of time_line with px by time value
            if (time > 549) { //if time value is greater than 549
                clearInterval(counterLine); //clear counterLine
            }
        }
    }

    function queCounter(index) {
        //creating a new span tag and passing the question number and total question
        let totalQueCounTag = '<span><p>' + index + '</p> of <p>' + questions.length + '</p> Questions</span>';
        bottom_ques_counter.innerHTML = totalQueCounTag;  //adding new span tag inside bottom_ques_counter
    }
}

