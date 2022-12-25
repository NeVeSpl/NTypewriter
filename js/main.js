function BlazorScrollToId(event, id)
{
    event.preventDefault();
    const element = document.getElementById(id);
    if (element instanceof HTMLElement)
    {
        element.scrollIntoView({
            behavior: "smooth",
            block: "end",
          
        });
    }
}