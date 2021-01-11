
// get data not working at the moment 
async function getData(){
    const response = await  fetch('quizQuestionTest.csv');
    const data = await response.text()
    
    const rows = data.split('\n').slice(1);
    rows.forEach( element => {
        const row = element.split(',').toLocaleString();
        
        const que = row[0];
        const option1 = row[1];
        const option2 = row[2];
        const option3 = row[3];
        const ans = row[4];


            let questions;
         
            questions = [
                {
                    numb: i,
                    questions: que,
                    answer: ans,
                    options: [
                        option1,
                        option2,
                        option3
                    ]
                }
            ];
        
    }
    
    )
}

// create array of number, questions, answer and options
let questions = [
    {
        numb: 1,
        question: "first question?",
        answer: "correct answer",
        options: [
            "incorrect answer",
            "correct answer",
            "incorrect answer",
            
        ]
    },
    {
        numb: 2,
        question: "second question?",
        answer: "correct answer",
        options: [
            "correct answer",
            "incorrect answer",
            "incorrect answer",
        ]
    },
    {
        numb: 3,
        question: "third question?",
        answer: "correct answer",
        options: [
            "incorrect answer",
            "correct answer",
            "incorrect answer",

        ]
    },
    {
        numb: 4,
        question: "fourth question?",
        answer: "correct answer",
        options: [
            "incorrect answer",
            "incorrect answer",
            "correct answer",
        ]
    },
    {
        numb: 5,
        question: "fifth question?",
        answer: "correct answer",
        options: [
            "incorrect answer",
            "correct answer",
            "incorrect answer",
        ]
    },
   
];