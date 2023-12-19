function SortBatch(Coloumn) {
    var BatchTable, Rows, ColSorting, i, row1, row2, Sort, Direction, Count;
    Count = 0;
    BatchTable = document.getElementById("BatchTable");
    BatchTable.find('tr').sort(function (a, b) {
        row1 = a.find('td:eq(' + Coloumn + ')').test();
        row1 = b.find('td:eq(' + Coloumn + ')').test();
    })
    ColSorting = true;
    Direction = "Ascending";
    Rows = BatchTable.rows;
}


//This function calculates the time spent on each event for Batch in the List<BatchLog>.
function calculateTimePerEvent(batchLogs) {
    //This part initializes the "eventsByBatch"-object and the 3 other variables needed for the function to work. 
    const eventsByBatch = {};
    let timeStarted;
    let timeCompleted;
    let currentBatchId = null;

    for (let i = 0; i < batchLogs.length - 1; i++) {
        const currentLog = batchLogs[i];

        //This part resets the variables for each new batch
        if (currentBatchId !== currentLog.BatchId) {
            currentBatchId = currentLog.BatchId;
            timeStarted = 0;
            timeCompleted = 0;
        }
        //This part sets the "timeCompleted" variable if the Event_Type is "Batch Completed".
        if (currentLog.Event_Type === "Batch Completed") {
            timeCompleted = currentLog.Time;

            //This part sets the "timeStarted" variable if the Event_Type is "Manual Start".
        } else if (currentLog.Event_Type === "Manual Start") {
            timeStarted = currentLog.Time;
        } else {
            //This part calculates the time spent on the event, if it's not the "Batch Completed" event.
            //This starts by inserting the current Batch ID into the "eventsByBatch"-object, if the current Batch ID isn't there. 
            if (!eventsByBatch.hasOwnProperty(currentBatchId)) {
                eventsByBatch[currentBatchId] = {};
            }

            //The time spent on the event is calculated and placed into the "eventsByBatch"-object. 
            const timeSpent = getTimeDifference(currentLog.Time, batchLogs[i + 1].Time);

            if (!eventsByBatch[currentBatchId].hasOwnProperty(currentLog.Event_Type)) {
                eventsByBatch[currentBatchId][currentLog.Event_Type] = 0;
            }

            eventsByBatch[currentBatchId][currentLog.Event_Type] += timeSpent;
        }
    }
    //Finally, the "eventsByBatch"-object, as well as the "timeStarted" and the "timeCompleted" variables are returned. 
    return { eventsByBatch, timeStarted, timeCompleted };
}
//This function gets the time difference between two TimeOnly variables. 
function getTimeDifference(startTime, endTime) {
    const startDateTime = new Date(`2000-01-01T${startTime}`);
    const endDateTime = new Date(`2000-01-01T${endTime}`);

    let timeDifferenceInSeconds;

    //This part checks whether or not the start time is later than the end time and calculates the time difference in seconds. 
    if (startDateTime > endDateTime) {
        const midnightDateTime = new Date(`2000-01-02T00:00:00`);
        timeDifferenceInSeconds = (midnightDateTime - startDateTime + endDateTime) / 1000;
    } else {
        timeDifferenceInSeconds = (endDateTime - startDateTime) / 1000;
    }

    return timeDifferenceInSeconds;
}

//This function calculates all of the Efficiency Rates for all of the Completed Batches. 
//That is to say, all of the Batches with the "Batch Completed" event. 
function calculateEfficiencyRate(eventsByBatchAndTime) {
    //This part initializes the "analyticsByBatch"-object needed to store each Batch' calculated data. 
    const analyticsByBatch = {};

    //This part terates through each batch in the given "eventsByBatch"-object. 
    for (const batchId in eventsByBatchAndTime.eventsByBatch) {
        //This part checks if the Batch ID is present in the "eventsByBatch"-object. 
        if (eventsByBatchAndTime.eventsByBatch.hasOwnProperty(batchId)) {
            //This part gets the time spent on each event in the given Batch, 
            //as well as the start time and end time of the Batch. 
            const batchEvents = eventsByBatchAndTime.eventsByBatch[batchId];
            const timeStarted = eventsByBatchAndTime.timeStarted;
            const timeCompleted = eventsByBatchAndTime.timeCompleted;

            //This part checks whether or not "Batch Completed" event is present for the batch. 
            //If it is present, the analysis begins. If not, the analysis of the Batch is skipped. 
            if (batchEvents["Batch Completed"]) {
                //This part gets the total amount of beers and the successful amount of beers
                //from the "Batch Completed" event's Description. 
                const description = batchEvents["Batch Completed"].Description;
                const producedStartIndex = description.indexOf("The total amount of beer produced is: ");
                const producedEndIndex = description.indexOf(".", producedStartIndex);
                const producedValue = description.substring(producedStartIndex, producedEndIndex);
                const totalBeers = parseInt(producedValue, 10);

                const producedGoodStartIndex = description.indexOf("The amount of successful beer produced is: ");
                const producedGoodEndIndex = description.indexOf(".", producedGoodStartIndex);
                const producedGoodValue = description.substring(producedGoodStartIndex, producedGoodEndIndex);
                const successfulBeers = parseInt(producedGoodValue, 10);

                //This part calculates the total amount of time spent on the Batch. 
                const timeInTotal = getTimeDifference(timeStarted, timeCompleted);
                let timeStopped = 0;

                //This part adds all of the time stopped for each of the various "Stopping"-events together. 
                if (batchEvents["Manual Stop"] !== undefined) {
                    timeStopped += batchEvents["Manual Stop"];
                }

                if (batchEvents["Empty inventory"] !== undefined) {
                    timeStopped += batchEvents["Empty inventory"];
                }

                if (batchEvents["Maintenance needed"] !== undefined) {
                    timeStopped += batchEvents["Maintenance needed"];
                }

                if (batchEvents["Motor power loss"] !== undefined) {
                    timeStopped += batchEvents["Motor power loss"];
                }

                //This part calculates the actual time spent on the Batch by removing the time stopped 
                //from the total time spent on the Batch. 
                const actualTimeSpent = timeInTotal - timeStopped;

                //This part calculates the success rate, Rate of Efficiency, 
                //and stores the results for the Batch in the "analyticsByBatch" - object
                const successRate = successfulBeers / totalBeers;
                const rateOfEfficiency = successRate / actualTimeSpent;
                analyticsByBatch[batchId] = {
                    timeInTotal,
                    successRate,
                    actualTimeSpent,
                    rateOfEfficiency,
                };
            }
        }
    }

    // Return the object containing calculated efficiency metrics for each batch
    return analyticsByBatch;
}

//This part waits for the page to load before functions are run. 
document.addEventListener("DOMContentLoaded", function () {
    const eventsByBatch = calculateTimePerEvent(batchLogs);
    const analyticsByBatch = calculateEfficiencyRate(eventsByBatch);
});