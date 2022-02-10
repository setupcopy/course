
export const sseTestApi = () => {
    const eventSource = new EventSource("/api/menu/sse");
    eventSource.addEventListener('eventTest',(event) => {
        console.log(event);
        eventSource.close();
    })
}