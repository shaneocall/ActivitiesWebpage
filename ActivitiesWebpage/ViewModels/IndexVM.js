var urlPath = window.location.pathname;

$(function () {
    ko.applyBindings(indexVM);
    indexVM.loadActivities();
});

var indexVM = {
    Activities: ko.observableArray([]),

    loadActivities: function () {
        var self = this;
        //Ajax Call Get All Articles
        $.ajax({
            type: "GET",
            url: urlPath + '/GetJson',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                self.Activities(data); //Put the response in ObservableArray
            },
            error: function (err) {
                alert(err.status + " : " + err.statusText);
            }
        });

    }
};

function Activities(Activities) {
    this.Id = ko.observable(Activities.Id);
    this.Name = ko.observable(Activities.Name);
    this.DateStarted = ko.observable(Activities.DateStarted);
    this.DateEnded = ko.observable(Activities.DateEnded);
    this.GamesPlayed = ko.observable(Activities.GamesPlayed);
}