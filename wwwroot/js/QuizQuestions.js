/*const quest = [];
console.log(quest);
const option1 = [];
const option2 =[];
const option3 = [];
const ans = [];
console.log(ans);



async function getData(){
    const response = await  fetch('quizQuestionTest.csv');
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

async function question() {
    await getData()
    let questions = [
        {
            numb: 1,
            questions: quest[0],
            answer: ans[0],
            options: [
                option1[0],
                option2[0],
                option3[0]
            ]
        },
        {
            numb: 2,
            questions: quest[1],
            answer: ans[1],
            options: [
                option1[1],
                option2[1],
                option3[1]
            ]
        },
        {
            numb: 3,
            questions: quest[2],
            answer: ans[2],
            options: [
                option1[2],
                option2[2],
                option3[2]
            ]
        },
        {
            numb: 4,
            questions: quest[3],
            answer: ans[3],
            options: [
                option1[3],
                option2[3],
                option3[3]
            ]
        },

    ];
}*/
