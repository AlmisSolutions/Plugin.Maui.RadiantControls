namespace Plugin.Maui.RadiantControls.Extensions;

public static class IEnumerableExtensions
{
    /// <summary>
    /// Finds the index of a given item in an IEnumerable<T>.
    /// </summary>
    /// <typeparam name="T">The type of items in the IEnumerable.</typeparam>
    /// <param name="enumerable">The IEnumerable to search.</param>
    /// <param name="item">The item to find the index of.</param>
    /// <returns>Returns the zero-based index of the item if found; otherwise, -1.</returns>
    public static int IndexOf<T>(this IEnumerable<T> enumerable, T item)
    {
        int index = 0;
        foreach (var current in enumerable)
        {
            if (current.Equals(item))
            {
                return index;
            }
            index++;
        }
        return -1;
    }
}

