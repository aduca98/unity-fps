function dateToUTC(date, time) {
    // Expect date as dd mm yyyy and time as 00:00PM
    var dateArr = date.split("/");
    var hour = parseInt(time.substring(0, 2));
    var minutes = parseInt(time.substring(3, 5)); // Skip over the :
    var pmOrAm = time.substring(5, 7);
    if(pmOrAm == "PM") {
        hour += 12;
    }
    console.log(dateArr, hour, minutes)
    var date = new Date(parseInt(dateArr[2]), parseInt(dateArr[0] - 1), parseInt(dateArr[1] - 1), hour, minutes);
    d = date.toUTCString();
    console.log(d);

    return date;
}

var date = "05/14/2017";
var time = "05:48PM";

var d = dateToUTC(date,time);
console.log(d)
console.log(d.getHours())