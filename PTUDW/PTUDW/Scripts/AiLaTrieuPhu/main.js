var listAllQuestion = [
  {
    question: "Xe nào nhiều bánh nhất?",
    answers: [
      "Xe Container",
      "Xe Tàu Lửa",
      "Xe Bánh Bao",
      "Xe Bánh Bò",
    ],
    toAnswer: "Xe Bánh Bao",
  },
  {
    question: "Bà Bảy bán bánh xèo sáng nay ăn gì?",
    answers: [
      "Bún Bò",
      "Bún Sứa",
      "Cháo Heo",
      "Cám Heo",
    ],
    toAnswer: "Bún Bò",
  },
  {
      question: "Bé Thi và Cu Thịnh là con của ai?",
      answers: [
          "Ông hàng xóm",
          "Ông thu tiền nước",
          "Ông thu tiền điện",
          "Ai cũng được",
      ],
      toAnswer: "Ai cũng được",
  },
  {
    question: "Hai Bà Trưng đi đánh giặc bằng phương tiện gì?",
    answers: [
      "SH",
      "Vespa",
      "Xích thố",
      "Voi",
    ],
    toAnswer: "Voi",
  },
  {
      question: "Con gái thích uống gì nhất?",
      answers: [
          "Trà sữa",
          "Trà đào",
          "Nước mía",
          "Bia",
      ],
      toAnswer: "Bia",
  },
  {
    question: "Trong bộ phim Lối Mòn Tội Lỗi, Rìu khờ móc túi vậy túi đó da gì",
    answers: ["Da cá sấu", "Da bà già", "Da thằn lằn", "Da rắn"],
    toAnswer: "Da rắn",
  },
  {
    question: "Sau khi ?",
    answers: ["<ul>", "<ol>", "<dl>", "<list>"],
    toAnswer: "<ul>",
  },
  {
    question: "Đâu là tên một ca khúc của Sơn Tùng MTP?",
    answers: [
      "Chạy ngay đi",
      "Trốn ngay đi",
      "Biến ngay đi",
      "Đi ngay đi",
    ],
    toAnswer: "Chạy ngay đi",
  },
  {
    question: "Từ nào không chỉ màu sắc?",
    answers: ["Đen sì", "Đen tuyền", "Đen nhánh", "Đen đủi"],
    toAnswer: "Đen đủi",
  },
  {
    question: "Người con gái đi lấy chồng gọi là gì?",
    answers: ["Xuất chiêu", "Xuất xưởng", "Xuất giá", "Xuất khẩu"],
    toAnswer: "Xuất giá",
  },
  {
    question: "Đâu là loại cháo khác với các món còn lại?",
    answers: ["Cháo gà", "Cháo bò", "Cháo heo", "Cháo vịt"],
    toAnswer: "Cháo bò",
  },
  {
    question: "Đâu là tên một loại mũ?",
    answers: [
      "Lưỡi hái",
      "Lưỡi Lê",
      "Lưỡi trai",
      "Lưỡi rắn",
    ],
    toAnswer: "Lưỡi trai",
  },
  {
    question:
      "Người ta thường nấu canh cua với gì?",
    answers: ["Củ cải", "Rau đay", "Mộc nhĩ", "Xúp lơ xanh"],
    toAnswer: "Củ cải",
  },
  {
    question: "Thính được làm từ gì?",
    answers: ["Gạo", "Mía", "Thịt", "Củ Cải"],
    toAnswer: "Gạo",
  },
  {
    question:
      "Hoa hậu nước nào sẽ trở thành Hoa Hậu Hoàn Vũ 2024?",
    answers: ["Colombia", "Philippin", "Mỹ", "A và B"],
    toAnswer: "Colombia",
  },
  {
    question: "Người like facebook được gì?",
    answers: ["Một năm may mắn", "Thi được điểm cao", "Thoát kiếp cô đơn", "Tất cả đều đúng"],
      toAnswer: "Tất cả đều đúng",
  },
  {
    question:
      "Bảy chú lùn làm nghề gì?",
    answers: ["Thợ rèn", "Thợ mỏ", "Thợ may", "Thợ săn"],
    toAnswer: "Thợ săn",
  },
  
];
var currentQuestion = 0;
const btnStartGame = document.getElementById("btn-Start-Game");
const contentQuestion = document.querySelector(".content__question");
const submitAnswer = document.querySelector(".submit-answer");
const btnSubmit = document.querySelector(".submit-answer button");
const question = document.getElementById("question");
const listAnswerCard = document.querySelectorAll(".answer__card");
const listAnswer = document.querySelectorAll(".answer");

const times = document.querySelector(".times");

const cardMoney = document.querySelectorAll(".money__card");
const listMoneyIcon = document.querySelectorAll(".money__icon");

const logo = document.querySelector(".logo img");

const container = document.querySelector(".container");
const aside = document.querySelector(".aside-wrap");
const toggleShowAside = document.querySelector(".toggler");

const help = document.querySelectorAll(".help__icon img");

const modal = document.querySelector(".modal");
const modalText = document.querySelector("#modal-text");
const btnResetGame = document.querySelector("#btn-reset");
const btnExitGame = document.querySelector("#btn-exit");

var initGameMoneyInter;
var answerRight = "";
var indexAnswerRight = -1;
var clickSubmit = false;
var listQuestion = [];
var listIndexQuestion = [];

// create files audio game
var audio;

const listMoneys = [
  "200",
  "400",
  "600",
  "1.000",
  "2.000",
  "3.000",
  "6.000",
  "10.000",
  "14.000",
  "22.000",
  "30.000",
  "40.000",
  "60.000",
  "85.000",
  "150.000",
];

function initGame() {
  let index = 15;
  let count = 1;
  initGameMoneyInter = setInterval(function () {
    index--;
    if (index >= 0) {
      cardMoney[index].classList.add("money__current");
      if (index < 14) {
        cardMoney[index + 1].classList.remove("money__current");
      }
    } else if (count > 0) {
      count--;
      index = 14;
      cardMoney[14].classList.add("money__current");
      cardMoney[0].classList.remove("money__current");
    } else {
      cardMoney[0].classList.remove("money__current");
      return;
    }
  }, 200);
}

// get a list have 15 question from data
function getListQuestion() {
  for (let i = 0; i < 16; i++) {
    let index = Math.floor(Math.random() * listAllQuestion.length);
    if (listIndexQuestion.indexOf(index) > -1) {
      i--;
      continue;
    }
    listIndexQuestion.push(index);
    listQuestion.push(listAllQuestion[index]);
  }
}

//

// show ever question out screen
// before show -> shuffe all answer of question

function showQuestion(index) {
  let objCurrentQuestion = listQuestion[index];

  // shuffe all answer in a question
  // C1:
  objCurrentQuestion.answers.sort((a, b) => 0.5 - Math.random());

  // show text
  question.textContent = objCurrentQuestion.question;
  listAnswer.forEach((answer, i) => {
    answer.textContent = objCurrentQuestion.answers[i];
    if (answer.textContent === objCurrentQuestion.toAnswer) {
      answerRight = answer.parentNode;
      indexAnswerRight = i;
    }
  });
}

function resetAll() {
  // restart money list
  clearInterval(initGameMoneyInter);
  cardMoney.forEach((card) => card.classList.remove("money__current"));
  for (
    let i = listMoneyIcon.length - currentQuestion - 1;
    i < listMoneyIcon.length;
    i++
  ) {
    listMoneyIcon[i].style.visibility = "hidden";
  }

  // restart help
    help[0].setAttribute("src", "../../Public/img/AiLaTrieuPhu/help_50_used.jpg");
    help[1].setAttribute("src", "../../Public/img/AiLaTrieuPhu/help_audience_used.jpg");
    help[2].setAttribute("src", "../../Public/img/AiLaTrieuPhu/help_audience_used.jpg");
  help.forEach((helper) => (helper.className = ""));
}

// show dialog modal to show result
function showModal(index) {
  modal.style.display = "block";
  let kq = "Bạn không trả lời đúng câu hỏi nào.";
  if (index) {
    kq =
      "Bạn đã chiến thắng trò chơi với phần thưởng " +
      listMoneys[index - 1] +
      ".000 đ";
  }
  modalText.textContent = kq;
}

function clearAnswerPlayer() {
  listAnswerCard.forEach((answer) => {
    answer.classList.remove("answer__player", "answer--right");
  });
}

function timesToAnswer() {
  let i = 30;
  times.textContent = 30;
  times.style.display = "block";

  let inter = setInterval(function () {
    if (i === 0 || clickSubmit) {
      clearInterval(inter);
      console.log("clear");

      return;
    }
    i--;
    times.textContent = i;
  }, 1000);
  setTimeout(() => {
    if (i === 0) {
      submitAnswer.style.opacity = 0;
        runAudio("../../Public/audio/end-time.ogg", true);
      alert("Ban da het thoi gian de tra loi cau hoi");
      alert("Ban dung lai voi " + currentQuestion + " cau hoi");
      resetAll();
      currentQuestion = 0;
      btnStartGame.style.display = "block";
      contentQuestion.style.display = "none";
      times.style.display = "none";
    }
  }, 30500);
}

function clearTwoAnswer() {
  // get index of answer will not clear
  let answerNoClear = -1;
  do {
    answerNoClear = Math.floor(Math.random() * 4);
  } while (answerNoClear === indexAnswerRight);

  // clear two answer rest
  listAnswer.forEach((answer, i) => {
    if (i !== indexAnswerRight && i !== answerNoClear) {
      answer.textContent = "";
    }
  });
}

// show background of aside when input checked
function showBackgroundAside() {
  // check width of brower
  // if brower width < 1100 => checkbox showed
  // continue check checkbox has checked?
  if (window.innerWidth < 1100 && toggleShowAside.checked === true) {
    aside.style.backgroundColor = "green";
  } else {
    aside.style.backgroundColor = "transparent";
  }
}

function runAudio(src, removeAudio) {
  if (removeAudio) {
    audio.pause();
  }
  audio = new Audio();
  audio.src = src;
  audio.play();
}

window.onload = function () {
  initGame();
  getListQuestion();
    runAudio("../../Public/audio/start.ogg", false);
};

btnStartGame.addEventListener("click", function (e) {
  // resetAll();
  clearInterval(initGameMoneyInter);
  cardMoney.forEach((card) => card.classList.remove("money__current"));

    runAudio("../../Public/audio/time_to_answer.ogg", true);
  this.style.display = "none";
  showQuestion(currentQuestion);
  contentQuestion.style.display = "block";
  help.forEach((helper) => (helper.className = "still"));
  clickSubmit = false;
  timesToAnswer();
});

listAnswerCard.forEach((answer) => {
  answer.addEventListener("click", function () {
    clearAnswerPlayer();
    answer.classList.add("answer__player");
    // submitAnswer.style.display = "block";
    submitAnswer.style.opacity = 1;
  });
});

help.forEach((helper, i) => {
  helper.addEventListener("click", function () {
    if (helper.className === "still") {
      switch (i) {
        case 0:
          clearTwoAnswer();
              helper.setAttribute("src", "../../Public/img/AiLaTrieuPhu/help_50_used.jpg");
          break;
        case 1:
              helper.setAttribute("src", "../../Public/img/AiLaTrieuPhu/help_audience_used.jpg");
          break;
        case 2:
              helper.setAttribute("src", "../../Public/img/AiLaTrieuPhu/help_audience_used.jpg");
          break;
      }
      helper.className = "";
    }
  });
});

btnSubmit.addEventListener("click", function () {
  let answerOfPlayer = document.querySelector(".answer__player .answer")
    .textContent;
  answerRight.classList.add("answer--right");
  clickSubmit = true;
  submitAnswer.style.opacity = 0;
  if (answerOfPlayer === listQuestion[currentQuestion].toAnswer) {
    let index = 14 - currentQuestion;

    listMoneyIcon[index].style.visibility = "visible";
    cardMoney[index].classList.add("money__current");
    if (currentQuestion !== 0) {
      cardMoney[index + 1].classList.remove("money__current");
    }
    if (currentQuestion === listQuestion.length - 1) {
        runAudio("../../Public/audio/winner.ogg", true);
      setTimeout(function () {
        // show modal
        showModal(currentQuestion);
        // btnStartGame.style.display = "block";
        contentQuestion.style.display = "none";
        times.style.display = "none";
        resetAll();
        audio.pause();
      }, 1500);
    } else {
      if (
        currentQuestion === 4 ||
        currentQuestion === 9 ||
        currentQuestion === 14
      ) {
          runAudio("../../Public/audio/answer_right_5_10.ogg", true);
      } else {
          runAudio("../../Public/audio/answer_right.ogg", true);
      }
      setTimeout(() => {
        currentQuestion++;
        showQuestion(currentQuestion);
        clickSubmit = false;
        timesToAnswer();
          runAudio("../../Public/audio/time_to_answer.ogg", true);
      }, 2500);
    }
  } else {
      runAudio("../../Public/audio/answer_wrong.ogg", true);
    setTimeout(function () {
      showModal(currentQuestion);
      resetAll();
      currentQuestion = 0;
      // btnStartGame.style.display = "block";
      contentQuestion.style.display = "none";
      times.style.display = "none";
      console.log("end");
    }, 2500);
  }
  setTimeout(clearAnswerPlayer, 2500);
});

// event exit game
btnExitGame.addEventListener("click", function () {
    
});

// event play again game
btnResetGame.addEventListener("click", function () {
  resetAll();
  btnStartGame.style.display = "block";
  modal.style.display = "none";
});
