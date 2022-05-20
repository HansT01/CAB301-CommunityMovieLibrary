﻿//CAB301 assessment 1 - 2022
//The implementation of MemberCollection ADT
using System;
using System.Linq;


public class MemberCollection : IMemberCollection
{
    // Fields
    private int capacity;
    private int count;
    private Member[] members; //make sure members are sorted in dictionary order

    // Properties

    // get the capacity of this member colllection 
    // pre-condition: nil
    // post-condition: return the capacity of this member collection and this member collection remains unchanged
    public int Capacity { get { return capacity; } }

    // get the number of members in this member colllection 
    // pre-condition: nil
    // post-condition: return the number of members in this member collection and this member collection remains unchanged
    public int Number { get { return count; } }

   


    // Constructor - to create an object of member collection 
    // Pre-condition: capacity > 0
    // Post-condition: an object of this member collection class is created

    public MemberCollection(int capacity)
    {
        if (capacity > 0)
        {
            this.capacity = capacity;
            members = new Member[capacity];
            count = 0;
        }
    }

    // check if this member collection is full
    // Pre-condition: nil
    // Post-condition: return ture if this member collection is full; otherwise return false.
    public bool IsFull()
    {
        return count == capacity;
    }

    // check if this member collection is empty
    // Pre-condition: nil
    // Post-condition: return ture if this member collection is empty; otherwise return false.
    public bool IsEmpty()
    {
        return count == 0;
    }

    // Add a new member to this member collection
    // Pre-condition: this member collection is not full
    // Post-condition: a new member is added to the member collection and the members are sorted in ascending order by their full names;
    // No duplicate will be added into this the member collection
    public void Add(IMember member)
    {
        if (member == null || IsFull() || Search(member))
        {
            return;
        }
        int pos = count - 1;
        while ((pos >= 0) && (member.CompareTo(members[pos]) == -1))
        {
            members[pos + 1] = members[pos];
            pos--;
        }
        members[pos + 1] = (Member) member;
        count++;
    }

    // Remove a given member out of this member collection
    // Pre-condition: nil
    // Post-condition: the given member has been removed from this member collection, if the given meber was in the member collection
    public void Delete(IMember aMember)
    {
        if (aMember == null)
        {
            return;
        }

        int idx = -1;
        int l = 0;
        int r = count - 1;
        while (l <= r)
        {
            int m = (l + r) / 2;
            int comparison = aMember.CompareTo(members[m]);
            if (comparison == 0)
            {
                idx = m;
                break;
            }
            else if (comparison == 1)
            {
                l = m + 1;
            }
            else
            {
                r = m - 1;
            }
        }        
        if (idx == -1)
        {
            return;
        }

        for (int i = idx; i < count - 1; i++)
        {
            members[i] = members[i + 1];
        }
        count--;
    }

    // Search a given member in this member collection 
    // Pre-condition: nil
    // Post-condition: return true if this memeber is in the member collection; return false otherwise; member collection remains unchanged
    public bool Search(IMember member)
    {
        if (member == null)
        {
            return false;
        }

        int l = 0;
        int r = count - 1;
        while (l <= r)
        {
            int m = (l + r) / 2;
            int comparison = member.CompareTo(members[m]);
            if (comparison == 0)
            {
                return true;
            }
            else if (comparison == 1)
            {
                l = m + 1;
            }
            else
            {
                r = m - 1;
            }
        }
        return false;
    }

    // Remove all the members in this member collection
    // Pre-condition: nil
    // Post-condition: no member in this member collection 
    public void Clear()
    {
        for (int i = 0; i < count; i++)
        {
            this.members[i] = null;
        }
        count = 0;
    }

    // Return a string containing the information about all the members in this member collection.
    // The information includes last name, first name and contact number in this order
    // Pre-condition: nil
    // Post-condition: a string containing the information about all the members in this member collection is returned
    public string ToString()
    {
        string s = "";
        for (int i = 0; i < count; i++)
            s = s + members[i].ToString() + "\n";
        return s;
    }


}

