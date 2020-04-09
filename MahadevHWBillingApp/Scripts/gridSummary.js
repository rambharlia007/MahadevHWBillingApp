// x = $("div > div > div", ".grid-data").first().append('<h5> hfwdhfkwj </h5>')

function getGridSummary(startDate, endDate, message, selector) {
    var date = {
        1: "1st",
        2: "2nd",
        3: "3rd",
        4: "4th",
        5: "5th",
        6: "6th",
        7: "7th",
        8: "8th",
        9: "9th",
        10: "10th",
        11: "11th",
        12: "12th",
        13: "13th",
        14: "14th",
        15: "15th",
        16: "16th",
        17: "17th",
        18: "18th",
        19: "19th",
        20: "20th",
        21: "21st",
        22: "22nd",
        23: "23th",
        24: "24th",
        25: "25th",
        26: "26th",
        27: "27th",
        28: "28th",
        29: "29th",
        30: "30th",
        31: "31st"
    };

    var months = {
        1: "January",
        2: "February",
        3: "March",
        4: "April",
        5: "May",
        6: "June",
        7: "July",
        8: "August",
        9: "September",
        10: "October",
        11: "November",
        12: "December"
    };

    function getDayAndMonthName(data) {
        var dateArr = data.split("-");
        var result = {};
        result.year = dateArr[2];

        if (dateArr[0][0].includes("0")) {
            result.day = date[dateArr[0][1]];
        }
        else
            result.day = date[dateArr[0]];


        if (dateArr[1][0].includes("0")) {
            result.month = months[dateArr[1][1]];
        }
        else
            result.month = months[dateArr[1]];

        return `${result.day} ${result.month} ${result.year}`;
    }

    var startDateMessage = getDayAndMonthName(startDate);
    var endDateMessage = getDayAndMonthName(endDate);

    var id = "#" + selector + "_wrapper";
    var finalMessage = `<p class="grid-summary"> ${message} from ${startDateMessage} to ${endDateMessage} </p>`;
    $("div > div", id).first().empty();
    $("div > div", id).first().append(finalMessage);
}