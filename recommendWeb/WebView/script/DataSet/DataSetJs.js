
$(document).ready(function () {

    $.getJSON('../../../api/User/GetLimit')
        .done(function (data) {

            if (data == null) {
                return;
            }
            else {
                allUserInfo = data;

                for (var i in allUserInfo) {
                    singleUserInfo = allUserInfo[i];

                    var row = "<tr> <td>"+singleUserInfo.UserId+"</td></tr>"
                    

                    $("#user-info").append(row);
                }

                $.getJSON('../../../api/Movie/GetLimit')
                    .done(function (data) {

                        if (data == null) {
                            return;
                        }
                        else {
                            allMovieInfo = data;

                            for (var i in allMovieInfo) {
                                singleMovieInfo = allMovieInfo[i];

                                var row = "<tr>";

                                row += "<td>" + singleMovieInfo.MovieId + "</td>";

                                row += "<td>" + singleMovieInfo.Title + "</td>";

                                row += "<td>" + singleMovieInfo.Genres + "</td>";

                                row += "</tr>";

                                $("#movie-info").append(row);
                            }

                            $.getJSON('../../../api/Rating/GetLimit')
                                .done(function (data) {

                                    if (data == null) {
                                        return;
                                    }
                                    else {
                                        allRatingInfo = data;

                                        for (var i in allRatingInfo) {
                                            singleRatingInfo = allRatingInfo[i];

                                            var row = "<tr>";

                                            row += "<td>" + singleRatingInfo.UserId + "</td>";

                                            row += "<td>" + singleRatingInfo.MovieId + "</td>";

                                            row += "<td>" + singleRatingInfo.Rate + "</td>";

                                            row += "<td>" + singleRatingInfo.TimeStamp + "</td>";

                                            row += "</tr>";

                                            $("#rating-info").append(row);
                                        }

                                    }
                                });
                        }
                    
                    });
            }

        });

});


