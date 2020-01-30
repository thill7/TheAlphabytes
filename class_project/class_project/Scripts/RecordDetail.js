$(document).ready(() => {
    async function toggleModal() {
        var state = $("#statsModal").css("display") == "block";
        var modal = $("#statsModal");

        var d = $.Deferred();

        $(modal)
            .one(state ? 'shown.bs.modal' : 'hidden.bs.modal', d.resolve)
            .modal(state ? 'show' : 'hide');

        return d.promise();
    }

    async function updateModal(athleteName, athleteID) {
        $("#modalAthleteName").text(athleteName);
        
        var athleteRaceResults = await (async () => {
            return new Promise((resolve, reject) => {
                $.ajax({
                    url: "/Record/History/" + athleteID,
                    dataType: "json",
                    success: (data) => {
                        resolve(data);
                    },
                    error: (jqXHR, textStatus) => {
                        reject(textStatus);
                    }
                });
            });
        })();



        var data = [];

        athleteRaceResults.forEach((event) => {
            let newTrace = {
                x: event.Records.map(r => r.Date),
                y: event.Records.map(r => r.Time),
                mode: 'lines',
                name: event.Name
            };
            data.push(newTrace);
        });

        console.log(athleteRaceResults);

        var layout = {
            title: `Race Times for ${athleteName}`
        };

        Plotly.newPlot('modalGraphHolder', data, layout, { responsive: true });

        $("#statsModal").modal("show");
    }

    $(".athlete-stats-btn").click((e) => {
        let target = e.target;
        updateModal($(target).attr("data-athlete-name"), $(target).attr("data-athlete-id"));
    });
});